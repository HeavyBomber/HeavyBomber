using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BomberPunk.GameForms
{
    //class MainMenu : MenuForm
    //{
    //    /// <summary>
    //    /// Forma głownego menu gry
    //    /// </summary>
    //    public MainMenu(GameScreen screen)
    //        : base(screen)
    //    {
    //        // Create menu entries.
    //        GraphicMenuEntry startMenuEntry = new GraphicMenuEntry("start game", 0, this);
    //        GraphicMenuEntry optionsMenuEntry = new GraphicMenuEntry("options", 1, this);
    //        GraphicMenuEntry exitMenuEntry = new GraphicMenuEntry("exit", 2, this);

    //        // Hook up menu event handlers.
    //        startMenuEntry.Selected += StartMenuEntrySelected;
    //        optionsMenuEntry.Selected += OptionsMenuEntrySelected;
    //        exitMenuEntry.Selected += OnCancel;

    //        // Add entries to the menu.
    //        menuEntries.Add(startMenuEntry);
    //        menuEntries.Add(optionsMenuEntry);
    //        menuEntries.Add(exitMenuEntry);
    //    }
    //    #region Handle Input


    //    /// <summary>
    //    /// Event handler for when the Play Game menu entry is selected.
    //    /// </summary>
    //    void StartMenuEntrySelected(object sender, PlayerIndexEventArgs e)
    //    {
    //        StateManager.Instance.setState("gameplay");
    //    }


    //    /// <summary>
    //    /// Event handler for when the Options menu entry is selected.
    //    /// </summary>
    //    void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
    //    {
    //        //ScreenManager.AddScreen(new OptionsMenuScreen(ScreenManager.Game.Services), null);
    //    }


    //    /// <summary>
    //    /// When the user cancels the main menu, ask if they want to exit the sample.
    //    /// </summary>
    //    void OnCancel(object sender, PlayerIndexEventArgs e)
    //    {
    //        //Program.Exit();
    //    }


    //    #endregion

    //}
}
