[![Build Status](https://travis-ci.org/jefersonsv/ConsoleMenu.svg?branch=master)](https://travis-ci.org/jefersonsv/ConsoleMenu)
[![Build status](https://ci.appveyor.com/api/projects/status/69ittx88i9d94g6f?svg=true)](https://ci.appveyor.com/project/jefersonsv/consolemenu)
[![NuGet](https://img.shields.io/nuget/v/ConsoleMenu-choice.svg)](https://www.nuget.org/packages/ConsoleMenu-choice/)
[![Coverage Status](https://coveralls.io/repos/github/jefersonsv/ConsoleMenu/badge.svg?branch=master)](https://coveralls.io/github/jefersonsv/ConsoleMenu?branch=master)
[![MIT Licence](https://badges.frapsoft.com/os/mit/mit.svg?v=103)](https://opensource.org/licenses/mit-license.php)
# ConsoleMenu choice
Console menu system with keyborad arrows and mouse selection
## Features
* Can use up and down arrows
* Can use mouse move to hightlight and click
* Setup forecolor and backcolor of items
* Input a ```IEnumerable<T>``` of items and the components will the select ```IEnumerable<T>```

## Nuget packages
https://www.nuget.org/packages/ConsoleMenu-choice/
## Contributing
If you can, please contribute by reporting issues, discussing ideas, or submitting pull requests with patches and new features. We do our best to respond to all issues and pull requests within a day or two.
## Usage
Simple usage
``` C#
var choice = new Menu().Render(new string[] { "Option 1", "Option 2", "Option 3" });
```

Generics with action usage
``` C#
List<MenuItem> i = new List<MenuItem>();
i.Add(new MenuItem()
{
    Caption = "Blue item",
    Start = (Action)(() =>
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("blue chose option");
    })
});
i.Add(new MenuItem()
{
    Caption = "Yellow item",
    Start = (Action)(() =>
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("yellow chose option");
    })
});

var actionMenu = new Menu("ActionMenu");
var act = actionMenu.Render(i);
act.Start();
```

## Thanks to
- [Travis CI](https://travis-ci.org/)
- [Appveyor](https://www.appveyor.com/)
- [Open Source Initiative](https://opensource.org/)
- [NuGet](https://www.nuget.org)
- [Emoji](http://www.webpagefx.com/tools/emoji-cheat-sheet/)
- [Coveralls](https://coveralls.io)
- [Myself: Jeferson Tenorio](https://br.linkedin.com/in/jefersontenorio)
- Thanks for all :smile: