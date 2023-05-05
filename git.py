from os import system
from sys import argv

try:
    from colorama import Fore
except ModuleNotFoundError:
    print('install colorama by pip')
    exit(-1)
print(Fore.RED + '[*] Starting git python script...')
if len(argv) == 2:
    print(Fore.RED + '[*] Arguments accepted. Starting git...')
    system('git add ./')
    system(f'git commit -m "{argv[1]}" ')
    system('git push')
else:
    print('''
    No arguments;
    Example: python git.py "initial commit"; 
    use ", not '  !!!
    ''')
print(Fore.RED + '[*] Ended git python script...')