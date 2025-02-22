using System;

namespace Shared.DTOs;

public abstract class TransferObject : IEquatable<TransferObject>
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public override bool Equals(object obj) => Equals(obj as TransferObject);

    public bool Equals(TransferObject other) => other is not null && Id == other.Id && Guid == other.Guid;

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => Name;
}