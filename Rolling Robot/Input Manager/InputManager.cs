using Godot;
using System;
using System.Collections.Generic;


/// <summary>
/// A manager for all Input systems to be put in place.
/// </summary>
public partial class InputManager : Node
{
    public static InputManager Instance;

    public const string INPUT_STORAGE_PATH = "";

    public Dictionary<string, InputType> ActiveInputs;

    [Export] InputType[] TestInputs;

    public override void _Ready()
    {
        // Ensure Singleton
        if (Instance == null) Instance = this;
        else QueueFree();

        ActiveInputs = new Dictionary<string, InputType>();
        for(int i = 0; i < TestInputs.Length; i++) {
            
            if(!RegisterInput(TestInputs[i])) {
                GD.PrintErr($"Input with name \"{TestInputs[i].Name}\" already present in ActiveInputs.");
            }
        }
    }

    public override void _Input(InputEvent inputEvent)
    {
        foreach(var input in ActiveInputs.Values) {
            input.UpdateInputData(inputEvent);
        }
    }

    public static T GetData<T>(string inputName) {
        if(Instance.ActiveInputs.TryGetValue(inputName, out var input)) {
            if(input is InputType<T> tInput) return tInput.GetData();
        }

        throw new IndexOutOfRangeException($"\"{inputName}\" input of type {typeof(T)} not found in ActiveInputs");
    }

    public bool RegisterInput(InputType input) {
        if(ActiveInputs.ContainsKey(input.Name)) return false;

        ActiveInputs.Add(input.Name, input);
        return true;
    }

    public bool DeregisterInput(string inputName) {
        if(ActiveInputs.ContainsKey(inputName)) {
            ActiveInputs.Remove(inputName);
            return true;
        }

        return false;
    }
}

public abstract partial class InputType : Resource
{
    [Export] public string Name { get; private set;}
    public abstract void UpdateInputData(InputEvent inputEvent);
}

public abstract partial class InputType<T> : InputType
{
    public abstract T GetData();

    public static implicit operator T(InputType<T> input) => input.GetData();
}
