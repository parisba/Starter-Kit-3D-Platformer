using Godot;
using System;

public partial class HUD : CanvasLayer
{
    public void OnCoinCollected(int coins)
    {
        var coinLabel = GetNode<Label>("Coins");
        coinLabel.Text = coins.ToString();
    }
}