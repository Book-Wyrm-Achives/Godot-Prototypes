using Godot;
using System;
using System.Collections.Generic;


/// <summary>
/// A manager for all Input systems to be put in place.
/// </summary>
public partial class InputManager : Node
{
    public static InputManager Instance;
    public Dictionary<string, InputType> ActiveInputs;

    public const string INPUT_STORAGE_PATH = "";

    public override void _Ready() {
        // Ensure Singleton
        if(Instance == null) Instance = this;
        else QueueFree();

        
    }

    public void GetActiveInputsFromFolder(string path = INPUT_STORAGE_PATH) {

    }
}


public abstract partial class InputType : Resource {
    [Export] public string Name;

    protected abstract T GetValue<T>();
}

public abstract partial class InputType<T> : InputType
{
    protected override T2 GetValue<T2>() {
        if(GetValue() is T2 value) {
            return value;
        }
        throw new Exception("Type mismatch in InputType");
    }

    public abstract T GetValue();
    public static implicit operator T(InputType<T> input) => input.GetValue();
}