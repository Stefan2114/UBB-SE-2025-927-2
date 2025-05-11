#!/usr/bin/env python3
import os
import subprocess
import json
import re
import argparse
from datetime import datetime
import matplotlib.pyplot as plt
from collections import defaultdict, Counter
import pandas as pd

class GitContributionReporter:
    def __init__(self, repo_path='.', since=None, until=None):
        """
        Initialize the reporter with repository path and time constraints
        
        Args:
            repo_path: Path to the git repository
            since: Start date for analysis (format: YYYY-MM-DD)
            until: End date for analysis (format: YYYY-MM-DD)
        """
        self.repo_path = repo_path
        self.since = "2025-04-23"
        self.until = "2025-05-05"
        
        # Change to repository directory
        self.original_dir = os.getcwd()
        os.chdir(self.repo_path)
        
        # Validate it's a git repository
        if not os.path.isdir('.git'):
            os.chdir(self.original_dir)
            raise ValueError(f"{repo_path} is not a git repository")
            
        # Get all contributors
        self.contributors = self._get_contributors()
        
    def __del__(self):
        """Return to original directory when object is deleted"""
        try:
            os.chdir(self.original_dir)
        except:
            pass
            
    def _run_git_command(self, command):
        """Run a git command and return the output"""
        try:
            result = subprocess.run(
                command,
                shell=True,
                cwd=self.repo_path,
                capture_output=True,
                encoding='utf-8',
                errors='replace',  # Replace undecodable chars
                check=True
            )
            return result.stdout.strip().strip("'\"")  # Remove extra quotes
        except subprocess.CalledProcessError as e:
            print(f"❌ Error running command: {command}")
            print(f"❌ Error message: {e.stderr}")
            return ""
            
    def _get_contributors(self):
        """Get list of all contributors (grouped by name similarity)"""
        cmd = "git shortlog -sne"
        if self.since:
            cmd += f" --since={self.since}"
        if self.until:
            cmd += f" --until={self.until}"

        output = self._run_git_command(cmd)
        if not output:
            return []

        # First pass: collect all contributors
        all_contributors = []
        for line in output.split('\n'):
            if not line.strip():
                continue

            parts = re.match(r'^\s*(\d+)\s+(.*?)\s+<(.*)>$', line)
            if parts:
                commits, name, email = parts.groups()
                all_contributors.append({
                    'name': name.strip(),
                    'email': email.strip().lower(),
                    'commits': int(commits)
                })

        # Helper function to normalize a name for comparison
        def normalize_name(name):
            """Convert name to lowercase, remove special chars, and sort words"""
            # Remove special characters and convert to lowercase
            clean_name = re.sub(r'[^a-zA-Z0-9\s]', '', name.lower())
            # Split into words and sort them to handle different order
            return ' '.join(sorted(clean_name.split()))

        # Second pass: group contributors by name similarity
        contributor_map = {}
        name_to_key = {}  # Maps normalized names to canonical keys

        for contributor in all_contributors:
            name = contributor['name']
            email = contributor['email']
            norm_name = normalize_name(name)
            
            # Look for a matching name group
            found_key = name_to_key.get(norm_name)
            
            # If no name match, check if the email username portion matches any existing name
            if not found_key:
                email_username = email.split('@')[0].lower()
                
                # Check for username in existing names
                for existing_name, existing_key in name_to_key.items():
                    existing_words = existing_name.split()
                    if (any(email_username in word.lower() for word in existing_words) or
                        any(word.lower() in email_username for word in existing_words)):
                        found_key = existing_key
                        name_to_key[norm_name] = found_key  # Remember this mapping
                        break
            
            # If still no match found, create a new entry
            if not found_key:
                found_key = norm_name
                name_to_key[norm_name] = found_key
                contributor_map[found_key] = {
                    'name': name,  # Use the first encountered name as canonical
                    'email': email,  # Use the first encountered email as canonical
                    'emails': [email],
                    'names': [name],
                    'commits': 0
                }
            
            # Update the contributor data
            contributor_map[found_key]['commits'] += contributor['commits']
            
            # Add this email if not already present
            if email not in contributor_map[found_key]['emails']:
                contributor_map[found_key]['emails'].append(email)
            
            # Add this name variant if not already present
            if name not in contributor_map[found_key]['names']:
                contributor_map[found_key]['names'].append(name)

        # Convert to list format
        contributors = []
        for data in contributor_map.values():
            contributors.append({
                'name': data['name'],
                'email': data['email'],  # Original email
                'aliases': {
                    'names': data['names'],
                    'emails': data['emails']
                },
                'commits': data['commits']
            })

        return contributors


        
    def get_commit_frequency(self):
        """Get commit frequency patterns per contributor"""
        result = {}
        
        for contributor in self.contributors:
            name = contributor['name']
            email = contributor['email']
            
            cmd = f'git log --author="{name} <{email}>" --date=format:%Y-%m-%d --pretty=format:%ad'
            if self.since:
                cmd += f' --since={self.since}'
            if self.until:
                cmd += f' --until={self.until}'
                
            output = self._run_git_command(cmd)
            # Clean dates: remove quotes, whitespace, and empty strings
            dates = []
            for d in output.split('\n'):
                d = d.strip().strip("'\"")  # Remove all quotes and whitespace
                if d:  # Only keep non-empty strings
                    dates.append(d)
            
            # Count commits per day
            date_counter = Counter(dates)
            
            # Get first and last commit
            if dates:
                date_objects = []
                for d in dates:
                    d = d.strip().strip("'\"")[:10]  # Strip quotes and take only first 10 chars
                    try:
                        date_obj = datetime.strptime(d, '%Y-%m-%d')
                        date_objects.append(date_obj)
                    except ValueError as e:
                        raise Exception(f"⚠️ Could not parse date '{d}' for {name}: {e}")
                    except Exception as e:
                        raise Exception(f"Date: {d}")
                
                if date_objects:
                    first_commit = min(date_objects).strftime('%Y-%m-%d')
                    last_commit = max(date_objects).strftime('%Y-%m-%d')
                    time_span = (max(date_objects) - min(date_objects)).days + 1
                else:
                    first_commit = 'N/A'
                    last_commit = 'N/A'
                    time_span = 0
            else:
                first_commit = 'N/A'
                last_commit = 'N/A'
                time_span = 0
                
            result[name] = {
                'first_commit': first_commit,
                'last_commit': last_commit, 
                'active_days': len(date_counter),
                'time_span_days': time_span,
                'commits_per_day': contributor['commits'] / max(time_span, 1)
            }
            
        return result
    
    def get_lines_changed(self):
        """Get number of lines added/deleted per contributor"""
        result = {}
        
        for contributor in self.contributors:
            name = contributor['name']
            email = contributor['email']
            
            cmd = f"git log --author=\"{name} <{email}>\" --pretty=tformat: --numstat"
            if self.since:
                cmd += f" --since={self.since}"
            if self.until:
                cmd += f" --until={self.until}"
                
            output = self._run_git_command(cmd)
            
            additions = 0
            deletions = 0
            for line in output.split('\n'):
                if not line.strip():
                    continue
                    
                parts = line.split('\t')
                if len(parts) >= 2 and parts[0] != '-' and parts[1] != '-':
                    additions += int(parts[0])
                    deletions += int(parts[1])
                    
            result[name] = {
                'additions': additions,
                'deletions': deletions,
                'total': additions + deletions
            }
            
        return result
        
    def get_files_changed(self):
        """Get number and type of files changed per contributor"""
        result = {}
        
        for contributor in self.contributors:
            name = contributor['name']
            email = contributor['email']
            
            cmd = f"git log --author=\"{name} <{email}>\" --name-only --pretty=format:"
            if self.since:
                cmd += f" --since={self.since}"
            if self.until:
                cmd += f" --until={self.until}"
                
            output = self._run_git_command(cmd)
            
            all_files = [f for f in output.split('\n') if f.strip()]
            file_count = len(set(all_files))
            
            # Count file types
            file_types = Counter([os.path.splitext(f)[1].lower() for f in all_files if os.path.splitext(f)[1]])
            
            # Top file extensions
            top_extensions = file_types.most_common(5)
            
            result[name] = {
                'count': file_count,
                'types': dict(top_extensions)
            }
            
        return result
        
    def get_diagrams_created(self):
        """
        Estimate number of diagrams created/modified
        This is an approximation based on diagram file extensions
        """
        result = {}
        diagram_extensions = ['.drawio', '.dia', '.vsdx', '.puml', '.plantuml', '.iuml', '.png', '.svg', '.uml']
        
        for contributor in self.contributors:
            name = contributor['name']
            email = contributor['email']
            
            cmd = f"git log --author=\"{name} <{email}>\" --name-only --pretty=format:"
            if self.since:
                cmd += f" --since={self.since}"
            if self.until:
                cmd += f" --until={self.until}"
                
            output = self._run_git_command(cmd)
            
            all_files = [f for f in output.split('\n') if f.strip()]
            diagram_files = [f for f in all_files if any(f.lower().endswith(ext) for ext in diagram_extensions)]
            
            result[name] = len(set(diagram_files))
            
        return result
        
    def get_commit_frequency(self):
        """Get commit frequency patterns per contributor"""
        result = {}
        
        for contributor in self.contributors:
            name = contributor['name']
            email = contributor['email']
            cmd = f"git log --author=\"{name} <{email}>\" --date=format:'%Y-%m-%d' --pretty=format:'%ad'"
            if self.since:
                cmd += f" --since={self.since}"
            if self.until:
                cmd += f" --until={self.until}"
                
            output = self._run_git_command(cmd)
            dates = output.split('\n')
            
            # Count commits per day
            date_counter = Counter(dates)
            
            # Get first and last commit
            if dates:
                
                date_objects = []
                for d in dates:
                    d = d.strip().strip("'\"")  # Strip surrounding whitespace and quotes
                    if len(d) == 10:  # Ensure the date is of the form YYYY-MM-DD
                        try:
                            date_obj = datetime.strptime(d, '%Y-%m-%d')
                            date_objects.append(date_obj)
                        except ValueError:
                            print(f"⚠️ Invalid date format: '{d}'")
                    else:
                        print(f"⚠️ Skipping invalid date: '{d}'")
                if date_objects:
                    first_commit = min(date_objects).strftime('%Y-%m-%d')
                    last_commit = max(date_objects).strftime('%Y-%m-%d')
                    time_span = (max(date_objects) - min(date_objects)).days + 1
                else:
                    first_commit = 'N/A'
                    last_commit = 'N/A'
                    time_span = 0
            else:
                first_commit = 'N/A'
                last_commit = 'N/A'
                time_span = 0
                
            result[name] = {
                'first_commit': first_commit,
                'last_commit': last_commit, 
                'active_days': len(date_counter),
                'time_span_days': time_span,
                'commits_per_day': contributor['commits'] / max(time_span, 1)
            }
            
        return result
        
    def get_complexity(self):
        """
        Estimate code complexity contribution
        Based on file sizes and types
        """
        result = {}
        
        for contributor in self.contributors:
            name = contributor['name']
            email = contributor['email']
            
            # Get recent commit
            cmd = f"git log --author=\"{name} <{email}>\" -n 1 --pretty=format:'%H'"
            if self.since:
                cmd += f" --since={self.since}"
            if self.until:
                cmd += f" --until={self.until}"
                
            commit_hash = self._run_git_command(cmd)
            
            if not commit_hash:
                result[name] = {
                    'complexity_score': 0,
                    'factors': {}
                }
                continue
                
            # Complexity factors
            complexity_factors = defaultdict(int)
            
            # Check files changed
            cmd = f"git show --name-only --pretty=format: {commit_hash}"
            files = self._run_git_command(cmd).split('\n')
            
            for file in files:
                if not file.strip():
                    continue
                    
                # Check if file exists
                if not os.path.exists(file):
                    continue
                    
                # File size
                size = os.path.getsize(file)
                complexity_factors['file_size'] += size
                
                # Code complexity estimation based on file type
                ext = os.path.splitext(file)[1].lower()
                
                # Higher weights for more complex file types
                if ext in ['.cpp', '.hpp', '.c', '.h']:
                    complexity_factors['cpp_files'] += 1
                elif ext in ['.java', '.cs']:
                    complexity_factors['java_cs_files'] += 1
                elif ext in ['.py']:
                    complexity_factors['python_files'] += 1
                elif ext in ['.js', '.ts']:
                    complexity_factors['js_files'] += 1
                elif ext in ['.html', '.css']:
                    complexity_factors['web_files'] += 1
                elif ext in ['.sql']:
                    complexity_factors['db_files'] += 1
                elif ext in ['.xml', '.json', '.yaml', '.yml']:
                    complexity_factors['config_files'] += 1
                    
            # Simple complexity score (can be refined)
            complexity_score = (
                complexity_factors['file_size'] / 1024 + 
                complexity_factors['cpp_files'] * 5 +
                complexity_factors['java_cs_files'] * 4 +
                complexity_factors['python_files'] * 3 +
                complexity_factors['js_files'] * 3 +
                complexity_factors['web_files'] * 2 +
                complexity_factors['db_files'] * 4 +
                complexity_factors['config_files']
            )
            
            result[name] = {
                'complexity_score': complexity_score,
                'factors': dict(complexity_factors)
            }
            
        return result
        
    def get_code_review_participation(self):
        """Estimate code review participation"""
        result = {}
        
        # Look for review comments in commits
        for contributor in self.contributors:
            name = contributor['name']
            email = contributor['email']
            
            # Get review-related comments
            cmd = f"git log --all --pretty=format:'%an <%ae>: %s %b' | grep -i 'review\\|approve\\|LGTM\\|comment\\|suggestion'"
            output = self._run_git_command(cmd)
            
            # Count reviews by contributor
            review_count = 0
            for line in output.split('\n'):
                if name in line or email in line:
                    review_count += 1
                    
            result[name] = review_count
            
        return result
        
    def determine_contribution_level(self, metrics):
        """
        Determine if contribution level is non-negligible
        
        Args:
            metrics: Dictionary of contributor metrics
            
        Returns:
            Dictionary with contribution assessment
        """
        result = {}
        
        # Define thresholds for non-negligible work
        min_commits = 5
        min_lines_changed = 100
        min_active_days = 3
        
        for name in metrics:
            commit_count = metrics[name]['commit_count']
            lines_changed = metrics[name]['lines_changed']['total'] if 'total' in metrics[name]['lines_changed'] else 0
            active_days = metrics[name]['commit_frequency']['active_days'] if 'active_days' in metrics[name]['commit_frequency'] else 0
            
            is_non_negligible = (
                commit_count >= min_commits or
                lines_changed >= min_lines_changed or
                active_days >= min_active_days
            )
            
            # Contribution assessment
            assessment = {
                'is_non_negligible': is_non_negligible,
                'reasoning': []
            }
            
            if commit_count >= min_commits:
                assessment['reasoning'].append(f"Made {commit_count} commits (threshold: {min_commits})")
                
            if lines_changed >= min_lines_changed:
                assessment['reasoning'].append(f"Changed {lines_changed} lines of code (threshold: {min_lines_changed})")
                
            if active_days >= min_active_days:
                assessment['reasoning'].append(f"Active on {active_days} days (threshold: {min_active_days})")
                
            if not is_non_negligible:
                assessment['reasoning'].append("Did not meet minimum thresholds for non-negligible work")
                
            result[name] = assessment
            
        return result
    def get_branches_used(self):
        """Get branches each contributor worked on"""
        result = {}
        
        # Get all branches first (without extra quotes)
        cmd = "git for-each-ref --format=%(refname:short) refs/heads/"
        branches = [b.strip() for b in self._run_git_command(cmd).split('\n') if b.strip()]
        
        for contributor in self.contributors:
            name = contributor['name']
            email = contributor['email']
            contributor_branches = []
            
            for branch in branches:
                # Build the git log command without quoting the branch name
                cmd = f'git log --author="{name} <{email}>" {branch} -n 1'
                if self.since:
                    cmd += f' --since={self.since}'
                if self.until:
                    cmd += f' --until={self.until}'
                    
                output = self._run_git_command(cmd)
                if output:
                    contributor_branches.append(branch)
                    
            result[name] = contributor_branches
            
        return result
    
    def generate_visualizations(self, metrics, output_dir='.'):
        """Generate visualization charts for the report"""
        os.makedirs(output_dir, exist_ok=True)
        
        # 1. Commits per contributor
        plt.figure(figsize=(10, 6))
        names = list(metrics.keys())
        commits = [metrics[name]['commit_count'] for name in names]
        
        plt.bar(names, commits)
        plt.title('Commits per Contributor')
        plt.ylabel('Number of Commits')
        plt.xticks(rotation=45, ha='right')
        plt.tight_layout()
        plt.savefig(os.path.join(output_dir, 'commits_per_contributor.png'))
        plt.close()
        
        # 2. Lines changed per contributor
        plt.figure(figsize=(10, 6))
        additions = [metrics[name]['lines_changed'].get('additions', 0) for name in names]
        deletions = [metrics[name]['lines_changed'].get('deletions', 0) for name in names]
        
        width = 0.35
        x = range(len(names))
        
        plt.bar([i - width/2 for i in x], additions, width, label='Additions')
        plt.bar([i + width/2 for i in x], deletions, width, label='Deletions')
        
        plt.title('Lines Changed per Contributor')
        plt.ylabel('Number of Lines')
        plt.xticks(x, names, rotation=45, ha='right')
        plt.legend()
        plt.tight_layout()
        plt.savefig(os.path.join(output_dir, 'lines_changed.png'))
        plt.close()
        
        # 3. Active days timeline
        plt.figure(figsize=(12, 6))
        
        # Create a timeline of activity
        all_dates = set()
        contributor_dates = {}
        
        for name in names:
            if 'commit_frequency' in metrics[name]:
                first = metrics[name]['commit_frequency'].get('first_commit')
                last = metrics[name]['commit_frequency'].get('last_commit')
                if first != 'N/A' and last != 'N/A':
                    first_date = datetime.strptime(first, '%Y-%m-%d')
                    last_date = datetime.strptime(last, '%Y-%m-%d')
                    
                    contributor_dates[name] = (first_date, last_date)
                    
        if contributor_dates:
            # Sort contributors by first commit date
            sorted_contributors = sorted(contributor_dates.items(), key=lambda x: x[1][0])
            
            for i, (name, (start, end)) in enumerate(sorted_contributors):
                plt.plot([start, end], [i, i], 'o-', linewidth=2, markersize=8, label=name)
                
            plt.yticks(range(len(sorted_contributors)), [name for name, _ in sorted_contributors])
            plt.title('Contribution Timeline')
            plt.xlabel('Date')
            plt.grid(axis='x', linestyle='--', alpha=0.7)
            plt.tight_layout()
            plt.savefig(os.path.join(output_dir, 'contribution_timeline.png'))
            plt.close()
            
        # 4. Non-negligible work assessment
        plt.figure(figsize=(10, 6))
        non_negligible = [int(metrics[name]['contribution_assessment']['is_non_negligible']) for name in names]
        
        colors = ['green' if val else 'red' for val in non_negligible]
        plt.bar(names, non_negligible, color=colors)
        plt.title('Non-Negligible Work Assessment')
        plt.ylabel('Assessment (1=Yes, 0=No)')
        plt.xticks(rotation=45, ha='right')
        plt.yticks([0, 1], ['No', 'Yes'])
        plt.tight_layout()
        plt.savefig(os.path.join(output_dir, 'contribution_assessment.png'))
        plt.close()
        
    def generate_report(self, output_format='html', output_file=None):
        """
        Generate the complete contribution report
        
        Args:
            output_format: 'markdown', 'json', or 'html'
            output_file: Path to save the report
            
        Returns:
            The report content as a string
        """
        # Gather all metrics
        metrics = {}
        
        for contributor in self.contributors:
            name = contributor['name']
            metrics[name] = {}
            metrics[name]['email'] = contributor['email']
            
        # Populate metrics
        commit_counts = self.get_commit_count()
        branches_used = self.get_branches_used()
        lines_changed = self.get_lines_changed()
        files_changed = self.get_files_changed()
        diagrams = self.get_diagrams_created()
        commit_frequency = self.get_commit_frequency()
        complexity = self.get_complexity()
        code_reviews = self.get_code_review_participation()
        
        for name in metrics:
            metrics[name]['commit_count'] = commit_counts.get(name, 0)
            metrics[name]['branches_used'] = branches_used.get(name, [])
            metrics[name]['lines_changed'] = lines_changed.get(name, {'additions': 0, 'deletions': 0, 'total': 0})
            metrics[name]['files_changed'] = files_changed.get(name, {'count': 0, 'types': {}})
            metrics[name]['diagrams_created'] = diagrams.get(name, 0)
            metrics[name]['commit_frequency'] = commit_frequency.get(name, {})
            metrics[name]['complexity'] = complexity.get(name, {'complexity_score': 0})
            metrics[name]['code_reviews'] = code_reviews.get(name, 0)
            
        # Determine contribution level
        contribution_assessment = self.determine_contribution_level(metrics)
        for name in metrics:
            metrics[name]['contribution_assessment'] = contribution_assessment.get(name, {'is_non_negligible': False, 'reasoning': []})
            
        # Generate visualizations
        vis_dir = 'contribution_visualizations'
        self.generate_visualizations(metrics, vis_dir)
        
        # Generate the report based on format
        if output_format == 'json':
            report_content = json.dumps(metrics, indent=2)
        elif output_format == 'html':
            report_content = self._generate_html_report(metrics, vis_dir)
        else:  # Default to markdown
            report_content = self._generate_markdown_report(metrics, vis_dir)
            
        # Save to file if specified
        if output_file:
            with open(output_file, 'w', encoding='utf-8') as f:
                f.write(report_content)
                
        return report_content
        
    def get_commit_count(self):
        """Get commit count per contributor"""
        return {c['name']: c['commits'] for c in self.contributors}
    
    def _generate_markdown_report(self, metrics, vis_dir):
        """Generate markdown report"""
        report = []
        
        # Report header
        report.append("# Git Contribution Report\n")
        report.append(f"Generated on: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n")
        report.append(f"Repository: {os.path.basename(os.path.abspath(self.repo_path))}\n")
        
        if self.since or self.until:
            date_range = []
            if self.since:
                date_range.append(f"From: {self.since}")
            if self.until:
                date_range.append(f"To: {self.until}")
            report.append(f"Date Range: {', '.join(date_range)}\n")
            
        # Summary
        report.append("## Summary\n")
        report.append("| Contributor | Commits | Lines Changed | Files Changed | Branches | Diagrams | Non-Negligible Work |\n")
        report.append("| --- | --- | --- | --- | --- | --- | --- |\n")
        
        for name in sorted(metrics.keys(), key=lambda x: metrics[x]['commit_count'], reverse=True):
            data = metrics[name]
            non_negligible = "✅" if data['contribution_assessment']['is_non_negligible'] else "❌"
            
            report.append(f"| {name} | {data['commit_count']} | {data['lines_changed'].get('total', 0)} | {data['files_changed'].get('count', 0)} | {len(data['branches_used'])} | {data['diagrams_created']} | {non_negligible} |\n")
            
        # Visualizations
        report.append("\n## Visualizations\n")
        report.append("### Commits per Contributor\n")
        report.append(f"![Commits per Contributor]({vis_dir}/commits_per_contributor.png)\n")
        
        report.append("### Lines Changed\n")
        report.append(f"![Lines Changed]({vis_dir}/lines_changed.png)\n")
        
        report.append("### Contribution Timeline\n")
        report.append(f"![Contribution Timeline]({vis_dir}/contribution_timeline.png)\n")
        
        report.append("### Non-Negligible Work Assessment\n")
        report.append(f"![Non-Negligible Work Assessment]({vis_dir}/contribution_assessment.png)\n")
        
        # Detailed Analysis
        report.append("\n## Detailed Analysis\n")
        
        for name in sorted(metrics.keys(), key=lambda x: metrics[x]['commit_count'], reverse=True):
            data = metrics[name]
            report.append(f"### {name}\n")
            report.append(f"### {data['email']}\n")
            
            # Commit statistics
            report.append("#### Commit Statistics\n")
            report.append(f"- Total Commits: {data['commit_count']}\n")
            
            if 'commit_frequency' in data and data['commit_frequency']:
                freq = data['commit_frequency']
                if 'first_commit' in freq and freq['first_commit'] != 'N/A':
                    report.append(f"- First Commit: {freq['first_commit']}\n")
                if 'last_commit' in freq and freq['last_commit'] != 'N/A':
                    report.append(f"- Last Commit: {freq['last_commit']}\n")
                if 'active_days' in freq:
                    report.append(f"- Active Days: {freq['active_days']}\n")
                if 'time_span_days' in freq and freq['time_span_days'] > 0:
                    report.append(f"- Contribution Timespan: {freq['time_span_days']} days\n")
                if 'commits_per_day' in freq:
                    report.append(f"- Average Commits Per Active Day: {freq['commits_per_day']:.2f}\n")
                    
            # Lines changed
            if 'lines_changed' in data and data['lines_changed']:
                lines = data['lines_changed']
                report.append("\n#### Code Changes\n")
                report.append(f"- Lines Added: {lines.get('additions', 0)}\n")
                report.append(f"- Lines Deleted: {lines.get('deletions', 0)}\n")
                report.append(f"- Total Lines Changed: {lines.get('total', 0)}\n")
                
            # Branches
            if data['branches_used']:
                report.append("\n#### Branches Used\n")
                for branch in data['branches_used']:
                    report.append(f"- {branch}\n")
                    
            # Files changed
            if 'files_changed' in data and data['files_changed']:
                files = data['files_changed']
                report.append("\n#### Files Changed\n")
                report.append(f"- Total Files: {files.get('count', 0)}\n")
                
                if 'types' in files and files['types']:
                    report.append("- File Types:\n")
                    for ext, count in files['types'].items():
                        report.append(f"  - {ext or 'no extension'}: {count}\n")
                        
            # Diagrams
            if data['diagrams_created'] > 0:
                report.append(f"\n#### Diagrams Created/Modified: {data['diagrams_created']}\n")
                
            # Code Reviews
            if data['code_reviews'] > 0:
                report.append(f"\n#### Code Review Participation: {data['code_reviews']} reviews\n")
                
            # Contribution Assessment
            report.append("\n#### Contribution Assessment\n")
            assessment = data['contribution_assessment']
            report.append(f"- **Non-Negligible Work**: {'Yes' if assessment['is_non_negligible'] else 'No'}\n")
            
            if assessment['reasoning']:
                report.append("- Reasoning:\n")
                for reason in assessment['reasoning']:
                    report.append(f"  - {reason}\n")
                    
            report.append("\n---\n")
            
        return "".join(report)
        
    def _generate_html_report(self, metrics, vis_dir):
        """Generate HTML report"""
        # Create HTML header
        html = []
        html.append("<!DOCTYPE html>")
        html.append("<html lang='en'>")
        html.append("<head>")
        html.append("  <meta charset='UTF-8'>")
        html.append("  <meta name='viewport' content='width=device-width, initial-scale=1.0'>")
        html.append("  <title>Git Contribution Report</title>")
        html.append("  <style>")
        html.append("    body { font-family: Arial, sans-serif; line-height: 1.6; margin: 0; padding: 20px; max-width: 1200px; margin: 0 auto; }")
        html.append("    h1, h2, h3, h4 { color: #333; }")
        html.append("    table { border-collapse: collapse; width: 100%; margin-bottom: 20px; }")
        html.append("    th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }")
        html.append("    th { background-color: #f2f2f2; }")
        html.append("    tr:nth-child(even) { background-color: #f9f9f9; }")
        html.append("    .card { border: 1px solid #ddd; border-radius: 4px; padding: 15px; margin-bottom: 20px; }")
        html.append("    .non-negligible { color: green; font-weight: bold; }")
        html.append("    .negligible { color: red; }")
        html.append("    .viz-container { display: flex; flex-wrap: wrap; justify-content: space-around; }")
        html.append("    .viz-item { margin: 10px; max-width: 45%; }")
        html.append("    img { max-width: 100%; height: auto; }")
        html.append("  </style>")
        html.append("</head>")
        html.append("<body>")
        
        # Report header
        html.append(f"<h1>Git Contribution Report</h1>")
        html.append(f"<p>Generated on: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}</p>")
        html.append(f"<p>Repository: {os.path.basename(os.path.abspath(self.repo_path))}</p>")
        
        if self.since or self.until:
            date_range = []
            if self.since:
                date_range.append(f"From: {self.since}")
            if self.until:
                date_range.append(f"To: {self.until}")
            html.append(f"<p>Date Range: {', '.join(date_range)}</p>")
            
        # Summary
        html.append("<h2>Summary</h2>")
        html.append("<table>")
        html.append("<tr>")
        html.append("  <th>Contributor</th>")
        html.append("  <th>Commits</th>")
        html.append("  <th>Lines Changed</th>")
        html.append("  <th>Files Changed</th>")
        html.append("  <th>Branches</th>")
        html.append("  <th>Diagrams</th>")
        html.append("<th>Non-Negligible Work</th>")
        html.append("</tr>")

        # Add rows for each contributor
        for name in sorted(metrics.keys(), key=lambda x: metrics[x]['commit_count'], reverse=True):
            data = metrics[name]
            non_negligible = "✅ Yes" if data['contribution_assessment']['is_non_negligible'] else "❌ No"
            non_negligible_class = "non-negligible" if data['contribution_assessment']['is_non_negligible'] else "negligible"
            
            html.append("<tr>")
            html.append(f"  <td>{name}</td>")
            html.append(f"  <td>{data['commit_count']}</td>")
            html.append(f"  <td>{data['lines_changed'].get('total', 0)}</td>")
            html.append(f"  <td>{data['files_changed'].get('count', 0)}</td>")
            html.append(f"  <td>{len(data['branches_used'])}</td>")
            html.append(f"  <td>{data['diagrams_created']}</td>")
            html.append(f"  <td class='{non_negligible_class}'>{non_negligible}</td>")
            html.append("</tr>")
            
        html.append("</table>")
        
        # Visualizations
        html.append("<h2>Visualizations</h2>")
        html.append("<div class='viz-container'>")
        
        html.append("<div class='viz-item'>")
        html.append("<h3>Commits per Contributor</h3>")
        html.append(f"<img src='{vis_dir}/commits_per_contributor.png' alt='Commits per Contributor'>")
        html.append("</div>")
        
        html.append("<div class='viz-item'>")
        html.append("<h3>Lines Changed</h3>")
        html.append(f"<img src='{vis_dir}/lines_changed.png' alt='Lines Changed'>")
        html.append("</div>")
        
        html.append("<div class='viz-item'>")
        html.append("<h3>Contribution Timeline</h3>")
        html.append(f"<img src='{vis_dir}/contribution_timeline.png' alt='Contribution Timeline'>")
        html.append("</div>")
        
        html.append("<div class='viz-item'>")
        html.append("<h3>Non-Negligible Work Assessment</h3>")
        html.append(f"<img src='{vis_dir}/contribution_assessment.png' alt='Non-Negligible Work Assessment'>")
        html.append("</div>")
        
        html.append("</div>")
        
        # Detailed Analysis
        html.append("<h2>Detailed Analysis</h2>")
        
        for name in sorted(metrics.keys(), key=lambda x: metrics[x]['commit_count'], reverse=True):
            data = metrics[name]
            html.append(f"<div class='card'>")
            html.append(f"<h3>{name}</h3>")
            
            # Commit statistics
            html.append("<h4>Commit Statistics</h4>")
            html.append("<ul>")
            html.append(f"<li>Total Commits: {data['commit_count']}</li>")
            
            if 'commit_frequency' in data and data['commit_frequency']:
                freq = data['commit_frequency']
                if 'first_commit' in freq and freq['first_commit'] != 'N/A':
                    html.append(f"<li>First Commit: {freq['first_commit']}</li>")
                if 'last_commit' in freq and freq['last_commit'] != 'N/A':
                    html.append(f"<li>Last Commit: {freq['last_commit']}</li>")
                if 'active_days' in freq:
                    html.append(f"<li>Active Days: {freq['active_days']}</li>")
                if 'time_span_days' in freq and freq['time_span_days'] > 0:
                    html.append(f"<li>Contribution Timespan: {freq['time_span_days']} days</li>")
                if 'commits_per_day' in freq:
                    html.append(f"<li>Average Commits Per Active Day: {freq['commits_per_day']:.2f}</li>")
            html.append("</ul>")
                    
            # Lines changed
            if 'lines_changed' in data and data['lines_changed']:
                lines = data['lines_changed']
                html.append("<h4>Code Changes</h4>")
                html.append("<ul>")
                html.append(f"<li>Lines Added: {lines.get('additions', 0)}</li>")
                html.append(f"<li>Lines Deleted: {lines.get('deletions', 0)}</li>")
                html.append(f"<li>Total Lines Changed: {lines.get('total', 0)}</li>")
                html.append("</ul>")
                
            # Branches
            if data['branches_used']:
                html.append("<h4>Branches Used</h4>")
                html.append("<ul>")
                for branch in data['branches_used']:
                    html.append(f"<li>{branch}</li>")
                html.append("</ul>")
                    
            # Files changed
            if 'files_changed' in data and data['files_changed']:
                files = data['files_changed']
                html.append("<h4>Files Changed</h4>")
                html.append("<ul>")
                html.append(f"<li>Total Files: {files.get('count', 0)}</li>")
                
                if 'types' in files and files['types']:
                    html.append("<li>File Types:")
                    html.append("<ul>")
                    for ext, count in files['types'].items():
                        html.append(f"<li>{ext or 'no extension'}: {count}</li>")
                    html.append("</ul>")
                    html.append("</li>")
                html.append("</ul>")
                        
            # Diagrams
            if data['diagrams_created'] > 0:
                html.append(f"<h4>Diagrams Created/Modified</h4>")
                html.append(f"<p>{data['diagrams_created']} diagrams</p>")
                
            # Code Reviews
            if data['code_reviews'] > 0:
                html.append(f"<h4>Code Review Participation</h4>")
                html.append(f"<p>{data['code_reviews']} reviews</p>")
                
            # Contribution Assessment
            assessment = data['contribution_assessment']
            non_negligible_class = "non-negligible" if assessment['is_non_negligible'] else "negligible"
            html.append("<h4>Contribution Assessment</h4>")
            html.append(f"<p class='{non_negligible_class}'><strong>Non-Negligible Work</strong>: {'Yes' if assessment['is_non_negligible'] else 'No'}</p>")
            
            if assessment['reasoning']:
                html.append("<p>Reasoning:</p>")
                html.append("<ul>")
                for reason in assessment['reasoning']:
                    html.append(f"<li>{reason}</li>")
                html.append("</ul>")
                    
            html.append("</div>")
            
        # Footer
        html.append("<hr>")
        html.append("<p><em>Generated by Git Contribution Reporter</em></p>")
        html.append("</body>")
        html.append("</html>")
        
        return "\n".join(html)

def main():
    """Main function for command-line interface"""
    parser = argparse.ArgumentParser(description='Generate a Git contribution report')
    parser.add_argument('--repo', type=str, default='.', help='Path to git repository (default: current directory)')
    parser.add_argument('--since', type=str, help='Start date for analysis (YYYY-MM-DD)')
    parser.add_argument('--until', type=str, help='End date for analysis (YYYY-MM-DD)')
    parser.add_argument('--format', choices=['markdown', 'json', 'html'], default='markdown', help='Output format (default: markdown)')
    parser.add_argument('--output', type=str, help='Output file path')
    
    args = parser.parse_args()
    
    try:
        reporter = GitContributionReporter(args.repo, args.since, args.until)
        report = reporter.generate_report(args.format, args.output)
        
        if not args.output:
            print(report)
        else:
            print(f"Report generated successfully: {args.output}")
            
    except Exception as e:
        print(f"Error: {e}")
        return 1
        
    return 0

if __name__ == "__main__":
    exit(main())