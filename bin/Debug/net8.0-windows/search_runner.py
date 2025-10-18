import pyautogui
import time
import os
import random
import json
import sys

search_dict = {
    1: "Best crypto scalping strategies October 2025",
    2: "How to use MACD and RSI for crypto entry signals",
    3: "Top crypto grid bots with backtesting tools",
    4: "Bitcoin vs Ethereum transaction speed comparison",
    5: "Crypto arbitrage opportunities in Indian exchanges",
    6: "How to automate trades using TradingView and 3Commas",
    7: "Best DeFi platforms for staking in 2025",
    8: "Risk management strategies for short-term crypto trading",
    9: "How to use trailing stop loss in volatile crypto markets",
    10: "Crypto tax rules in India 2025 simplified",
    11: "Best crypto exchanges with low fees India",
    12: "How to use Bollinger Bands and RSI together",
    13: "Ethereum gas fees vs Solana transaction cost",
    14: "Top altcoins with staking rewards 2025",
    15: "Bitcoin halving impact on price history",
    16: "Crypto grid bot settings for sideways market",
    17: "How to use smart trade bots for compounding",
    18: "Best crypto wallets for Android 2025",
    19: "How to avoid liquidation in margin trading",
    20: "Crypto trading psychology tips for beginners",
    21: "How to use Fibonacci retracement in crypto",
    22: "Best crypto coins for long-term holding",
    23: "How to backtest crypto strategies on TradingView",
    24: "Crypto portfolio diversification strategies",
    25: "How to use DCA bots effectively in bear market",
    26: "Class 12 Physics ray optics derivations PDF",
    27: "Important organic chemistry mechanisms for JEE",
    28: "Python Class 12 file handling programs with output",
    29: "Electrochemistry Class 12 NCERT summary",
    30: "Class 12 Maths integration tricks and formulas",
    31: "Modern physics one-shot revision notes for JEE",
    32: "Class 12 Computer Science output-based questions PDF",
    33: "Thermodynamics MCQs with solutions JEE level",
    34: "Class 12 Chemistry reaction mechanism flowcharts",
    35: "Physics capacitor formulas and tricks Class 12",
    36: "Class 12 Physics electromagnetic waves notes",
    37: "Class 12 Chemistry p-block element summary",
    38: "Class 12 Maths probability solved examples",
    39: "Class 12 Computer Science CSV file handling",
    40: "Class 12 Physics dual nature of matter notes",
    41: "Class 12 Chemistry coordination compounds tricks",
    42: "Class 12 Maths differential equations shortcuts",
    43: "Class 12 Physics AC circuit formulas",
    44: "Class 12 Chemistry haloalkanes and haloarenes notes",
    45: "Class 12 Computer Science Python MCQs",
    46: "Class 12 Physics magnetism and matter summary",
    47: "Class 12 Chemistry alcohol phenol ether notes",
    48: "Class 12 Maths vector algebra formulas",
    49: "Class 12 Physics semiconductor devices notes",
    50: "Class 12 Chemistry biomolecules summary",
    51: "Class 12 Computer Science Python output questions",
    52: "Class 12 Maths 3D geometry formulas",
    53: "Class 12 Physics nuclear physics revision",
    54: "Class 12 Chemistry polymers and chemistry in everyday life",
    55: "Class 12 Computer Science Python project ideas",
    56: "Class 12 Physics practice questions for JEE",
    57: "Class 12 Chemistry organic conversion tricks",
    58: "Class 12 Maths matrices and determinants revision",
    59: "Class 12 Physics formula sheet PDF",
    60: "Class 12 Chemistry NCERT exemplar solutions",
    61: "Best custom ROMs for Redmi Note 5 Pro Wayred 2025",
    62: "Agni kernel tweaks for performance and battery",
    63: "Magisk modules for gaming and system optimization",
    64: "SafetyNet bypass methods for rooted Android 2025",
    65: "ADB commands for advanced Android modding",
    66: "SELinux tweaks for privacy-focused Android ROMs",
    67: "Top lightweight ROMs for legacy Android phones",
    68: "How to pass Play Integrity on rooted Android",
    69: "Enable camera2 API on rooted Android without Magisk",
    70: "Using Termux for kernel-level Android tweaks",
    71: "Best ROMs for Wayred with Agni kernel support",
    72: "How to flash custom recovery using fastboot",
    73: "MagiskHide alternatives for SafetyNet bypass",
    74: "How to root Android with patched boot image",
    75: "Best Android ROMs for battery life 2025",
    76: "How to install GApps on custom ROM",
    77: "How to fix bootloop after flashing kernel",
    78: "How to use OrangeFox recovery for ROM flashing",
    79: "How to backup and restore EFS partition",
    80: "How to enable OTA updates on rooted Android",
    81: "How to use Kernel Auditor for performance tuning",
    82: "How to install Xposed on Android 11",
    83: "How to use LSPosed for module injection",
    84: "How to pass CTS profile check on rooted Android",
    85: "How to spoof device fingerprint for Play Store",
    86: "How to install ROM via TWRP without data wipe",
    87: "How to fix encryption issues on custom ROM",
    88: "How to use Magisk Delta for advanced root control",
    89: "How to flash vendor image separately",
    90: "How to use adb shell for system tweaks",
    91: "How to install custom boot animation",
    92: "How to remove bloatware using adb",
    93: "How to enable VoLTE on custom ROM",
    94: "How to fix SafetyNet after Magisk update",
    95: "How to install OTA updates on rooted device",
    96: "How to use Viper4Android on custom ROM",
    97: "How to patch boot image using Magisk",
    98: "How to use Termux for automation scripts",
    99: "How to fix WiFi issues on custom ROM",
    100: "How to install Dolby Atmos on rooted Android",
    101: "Class 12 Physics optics numericals with solutions",
    102: "How to use ATR indicator in crypto trading",
    103: "Enable camera2 API on rooted Android without Magisk",
    104: "Class 12 Chemistry aldehydes and ketones summary",
    105: "Top crypto arbitrage bots with API support",
    106: "How to fix bootloop after flashing kernel",
    107: "Class 12 Maths integration by parts examples",
    108: "How to use LSPosed for module injection",
    109: "Crypto portfolio diversification strategies",
    110: "Class 12 Physics alternating current derivations",
    111: "How to use Fibonacci extensions in crypto",
    112: "Best ROMs for Wayred with Agni kernel support",
    113: "Class 12 Chemistry carboxylic acids important reactions",
    114: "Top crypto trading bots with smart orders",
    115: "Class 12 Maths probability past year questions",
    116: "How to use Kernel Auditor for performance tuning",
    117: "How to trade crypto using candlestick patterns",
    118: "Class 12 Computer Science Python recursion programs",
    119: "Best crypto coins for staking on Solana",
    120: "How to spoof device fingerprint for Play Store",
    121: "Class 12 Physics electromagnetic induction notes",
    122: "How to use stochastic oscillator in crypto trading",
    123: "Best Android ROMs for battery life 2025",
    124: "Class 12 Chemistry amines important conversions",
    125: "Top crypto screeners for altcoin analysis",
    126: "How to use OrangeFox recovery for ROM flashing",
    127: "Class 12 Maths differential equations solved examples",
    128: "How to use parabolic SAR in crypto trading",
    129: "Class 12 Physics wave optics interference derivations",
    130: "How to install Viper4Android on custom ROM",
    131: "Top crypto copy trading platforms 2025",
    132: "Class 12 Chemistry haloalkanes important mechanisms",
    133: "How to pass CTS profile check on rooted Android",
    134: "Class 12 Maths vector algebra solved examples",
    135: "How to use Heikin Ashi candles in crypto",
    136: "Class 12 Physics semiconductor devices numericals",
    137: "How to install GApps on custom ROM",
    138: "Class 12 Chemistry biomolecules important questions",
    139: "How to use VWAP in crypto scalping",
    140: "Class 12 Maths 3D geometry past year questions",
    141: "How to use adb shell for system tweaks",
    142: "Class 12 Computer Science Python string handling programs",
    143: "How to use Elliott Wave theory in crypto",
    144: "Class 12 Physics nuclear physics solved numericals",
    145: "How to remove bloatware using adb",
    146: "Class 12 Chemistry polymers important notes",
    147: "How to use RSI divergence in crypto trading",
    148: "Class 12 Maths matrices and determinants solved examples",
    149: "How to install custom boot animation",
    150: "Class 12 Computer Science Python list programs",
    151: "How to use Ichimoku Cloud in crypto trading",
    152: "Class 12 Physics dual nature of matter numericals",
    153: "How to enable VoLTE on custom ROM",
    154: "Class 12 Chemistry coordination compounds important questions",
    155: "How to use MACD histogram in crypto trading",
    156: "Class 12 Maths probability solved NCERT examples",
    157: "How to install ROM via TWRP without data wipe",
    158: "Class 12 Computer Science Python tuple programs",
    159: "How to use ADX indicator in crypto trading",
    160: "Class 12 Physics magnetism and matter derivations",
    161: "How to patch boot image using Magisk",
    162: "Class 12 Chemistry alcohols phenols ethers conversions",
    163: "How to use moving average envelopes in crypto",
    164: "Class 12 Maths integration tricks for JEE",
    165: "How to use Magisk Delta for advanced root control",
    166: "Class 12 Computer Science Python set programs",
    167: "How to use candlestick reversal patterns in crypto",
    168: "Class 12 Physics AC circuit solved numericals",
    169: "How to flash vendor image separately",
    170: "Class 12 Chemistry organic conversion practice",
    171: "How to use volume profile in crypto trading",
    172: "Class 12 Maths differential equations shortcuts",
    173: "How to fix encryption issues on custom ROM",
    174: "Class 12 Physics formula sheet quick revision",
    175: "How to use order blocks in crypto trading",
    176: "Class 12 Chemistry NCERT exemplar solutions",
    177: "How to install Dolby Atmos on rooted Android",
    178: "Class 12 Maths matrices past year questions",
    179: "How to use supply and demand zones in crypto",
    180: "Class 12 Physics wave optics solved examples",
    181: "How to install Xposed on Android 11",
    182: "Class 12 Chemistry carboxylic acids conversions",
    183: "How to use RSI and MACD together in crypto",
    184: "Class 12 Maths vector algebra formulas",
    185: "How to use Termux for automation scripts",
    186: "Class 12 Physics electromagnetic waves solved numericals",
    187: "How to use Bollinger Band squeeze strategy",
    188: "Class 12 Chemistry coordination compounds summary",
    189: "How to use Fibonacci retracement in crypto",
    190: "Class 12 Maths probability solved examples",
    191: "How to use TradingView alerts for crypto bots",
    192: "Class 12 Physics semiconductor devices notes",
    193: "How to use ATR stop loss in crypto trading",
    194: "Class 12 Chemistry haloalkanes conversions",
    195: "How to use stochastic RSI in crypto",
    196: "Class 12 Maths integration by substitution examples",
    197: "How to use smart trade bots for compounding",
    198: "Class 12 Physics nuclear physics derivations",
    199: "How to use parabolic SAR with MACD in crypto",
    200: "Class 12 Chemistry polymers solved examples",
}
def run_searches(num_queries: int, profile_key: int):
    """Run the sequence of searches."""
    print("STATUS: Starting Edge browser...")
    
    # Launch Edge with profile
    profiles = _detect_edge_profiles()
    profile_folder = profiles.get(profile_key)
    if profile_folder:
        os.system(f'start msedge --profile-directory="{profile_folder}"')
    else:
        os.system('start msedge')

    time.sleep(3)
    
    print("STATUS: Starting searches...")
    
    # Open a single new tab before starting
    pyautogui.hotkey("ctrl", "t")
    time.sleep(0.8)
    
    random_keys = random.sample(range(1, len(search_dict) + 1), num_queries)
    
    for i, k in enumerate(random_keys):
        query = search_dict[k]
        print(f"PROGRESS: {i+1}/{num_queries}")
        
        # Perform search in the existing tab
        pyautogui.hotkey("ctrl", "l")
        time.sleep(0.3)
        pyautogui.typewrite(query, interval=0.02)
        time.sleep(0.3)
        pyautogui.hotkey("enter")
        time.sleep(5)
    
    print("STATUS: Completed all searches")
    print("COMPLETED")

def _detect_edge_profiles():
    """Detect available Edge profiles - FIXED for 'Profile' with capital P"""
    local_app = os.getenv('LOCALAPPDATA') or os.path.expanduser('~\\AppData\\Local')
    user_data_dir = os.path.join(local_app, 'Microsoft', 'Edge', 'User Data')
    profiles = {}
    try:
        if os.path.isdir(user_data_dir):
            for name in os.listdir(user_data_dir):
                if name == 'Default':
                    profiles[0] = 'Default'
                elif name.startswith('Profile '):  # Fixed: exact match for "Profile "
                    try:
                        n = int(name.split(' ', 1)[1])  # Get number after "Profile "
                        profiles[n] = name
                    except Exception:
                        continue
    except Exception:
        pass
    return profiles

if __name__ == '__main__':
    # Get parameters from command line
    if len(sys.argv) != 3:
        print("ERROR: Usage: python search_runner.py <num_searches> <profile_key>")
        sys.exit(1)
    
    try:
        num_searches = int(sys.argv[1])
        profile_key = int(sys.argv[2])
        run_searches(num_searches, profile_key)
    except Exception as e:
        print(f"ERROR: {str(e)}")
        sys.exit(1)