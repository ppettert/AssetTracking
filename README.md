# Weekly MiniProject W40
## AssetTracker
### C# project done to learn Class inhertance and more.

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

The class Asset has these properties:
```
Asset Type: Defined by Asset subclasses Computer and Phone, inheriting Asset base class
Brand: String containing brand name, such as Apple, Samsung, Lenovo etc.
Price: Class with properties Amount (type:decimal) and Currency (type:enum with currency codes)
DatePurchased: Date (year, month, day) of the DateOnly type, typically handled in yyyy-MM-dd format
Office: Defined by enum Country that lists country names where there are offices with assets
```

![hello](images/vaultboy_sunglasses.png)
