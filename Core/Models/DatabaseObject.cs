using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public abstract class DatabaseObject : IEquatable<DatabaseObject>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public override bool Equals(object obj) => Equals(obj as DatabaseObject);

    public bool Equals(DatabaseObject other) => other is not null && Id == other.Id && Guid == other.Guid;

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => Name;
}
