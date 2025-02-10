using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models;

public abstract class DatabaseObject : IEquatable<DatabaseObject>
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public override bool Equals(object obj) => Equals(obj as DatabaseObject);

    public bool Equals(DatabaseObject other) => Id == other.Id && Guid == other.Guid;

    public override int GetHashCode() => base.GetHashCode();
}
