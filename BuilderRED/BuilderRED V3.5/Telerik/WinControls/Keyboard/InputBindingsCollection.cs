// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.InputBindingsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.WinControls.Keyboard
{
  [ListBindable(BindableSupport.No)]
  public class InputBindingsCollection : CollectionBase
  {
    private Shortcuts owner;

    internal Shortcuts Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    public InputBindingsCollection()
    {
    }

    public InputBindingsCollection(Shortcuts owner)
    {
      this.owner = owner;
    }

    public InputBindingsCollection(InputBindingsCollection value)
    {
      this.AddRange(value);
    }

    public InputBindingsCollection(InputBinding[] value)
    {
      this.AddRange(value);
    }

    public InputBinding this[int index]
    {
      get
      {
        return (InputBinding) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(InputBinding value)
    {
      return this.List.Add((object) value);
    }

    public void AddRange(InputBinding[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    public void AddRange(InputBindingsCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    public bool Contains(InputBinding value)
    {
      return this.List.Contains((object) value);
    }

    public void CopyTo(InputBinding[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public InputBindingsCollection GetBindingByComponent(IComponent component)
    {
      InputBindingsCollection bindingsCollection = new InputBindingsCollection();
      for (int index = 0; index < this.List.Count; ++index)
      {
        IComponent commandContext = ((InputBinding) this.List[index]).CommandContext as IComponent;
        if (commandContext != null && commandContext == component)
          bindingsCollection.Add((InputBinding) this.List[index]);
      }
      if (bindingsCollection.Count > 0)
        return bindingsCollection;
      return (InputBindingsCollection) null;
    }

    public void RemoveBindingByComponent(IComponent component)
    {
      InputBindingsCollection bindingByComponent = this.GetBindingByComponent(component);
      if (bindingByComponent == null)
        return;
      for (int index = 0; index < bindingByComponent.Count; ++index)
        this.Remove(bindingByComponent[index]);
    }

    public int IndexOf(InputBinding value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Insert(int index, InputBinding value)
    {
      this.List.Insert(index, (object) value);
    }

    public void Remove(InputBinding value)
    {
      this.List.Remove((object) value);
    }
  }
}
