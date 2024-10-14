# Weekly MiniProject W40
## AssetTracker
### C# project done to learn Class inhertance and more.

The Asset Tracker stores and displays assets of types Phone and Computer togther with Purchase Date and Price, Office location and markup of items close to write-off. It has built-in live currency conversion costs of assets purchased in EUR and SEK to USD.

User interface commands:
```
A - Add new asset to list
P - Print list sorted by Office
T - Print list sorted by Type
S - Show asset list stats
F - Fill asset list with test data
L - List commands
Q - Quit
```

Items marked $${\color{red}red}$$ if purchase date is 2 years and 6 months or more ago.
Items marked $${\color{magenta}magenta}$$ if purchase date is 2 years and 3 months or more ago.
Spec says $${\color{yellow}yellow}$$ should be used but it is hard to see when you have white background in terminal/console. 

Locale set to *"en-US"* because *LiveCurrency* class depends on a data source that uses "." decimal notation.

The procedure to add an Asset manually (Menu choice 'A') is as follows:

1. Select between Computer or Phone
2. Enter Brand name, such as Apple, Lenovo, Samsung, OnePlus, HP etc.
3. Enter Model name, such as Iphone, Macboook, Thinkpad, Galaxy 23 etc.
4. Enter Office, for which the asset was bought, pick between USA, Spain and Sweden.
5. Enter Price, a numeric value with decimals marked by a dot, not comma. Currency will be set according to Office, commas in en-US are interpreted as separator for thousands, i.e 1,000 for one-thousand.
6. Enter Purchase Date, in the format of YYYY-MM-DD. August 21, 2024 would be entered as 2024-08-21.

> [!TIP]
> It might help to drink coffee before reading AddAssetProperties() . No guarantees, though.

![hello](images/vaultboy_sunglasses.png)
