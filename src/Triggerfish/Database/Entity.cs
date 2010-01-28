using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triggerfish.Database
{
	/// <summary>
	/// Abstract class to represent a database entity with a
	/// primary key
	/// </summary>
	/// <typeparam name="TId">The primary key type</typeparam>
	public abstract class Entity<TId>
	{
		/// <summary>
		/// Accessor to the primary key value
		/// </summary>
		public virtual TId Id { get; private set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		protected Entity()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="id">The primary key value</param>
		protected Entity(TId id)
		{
			Id = id;
		}

		/// <summary>
		/// Compare this entity to another based on matching primary keys
		/// </summary>
		/// <param name="rhs">The other entity</param>
		/// <returns>True if equal, false otherwise</returns>
		public virtual bool Equals(Entity<TId> rhs)
		{
			if (ReferenceEquals(null, rhs))
				return false;
			if (ReferenceEquals(this, rhs))
				return true;
			return Equals(Id, rhs.Id);
		}

		/// <summary>
		/// Compare this entity to another object based 
		/// on that object being an entity and matching primary keys
		/// </summary>
		/// <param name="rhs">The other object</param>
		/// <returns>True if equal, false otherwise</returns>
		public override bool Equals(object rhs)
		{
			if (ReferenceEquals(null, rhs))
				return false;
			if (!(rhs is Entity<TId>))
				return false;
			return Equals(rhs as Entity<TId>);
		}

		/// <summary>
		/// Get the hash code for this entity
		/// </summary>
		/// <returns>A unique integer for this entity</returns>
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
