using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Objects;
using reflections.ilb.entity.Base;
using reflections.ilb.entity.Entities;

namespace reflections.ilb.entity
{
	public partial class ILBContext : DbContext
	{
		/// <summary>
		/// Constructs a new context instance
		/// </summary>
		public ILBContext()
			: base(ILBConnection.CreateConnection(), true)
		{
		}

		/// <summary>
		/// Constructs a new context instance
		/// </summary>
		/// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
		public ILBContext(string nameOrConnectionString)
			: base(nameOrConnectionString)
		{
		}

		/// <summary>
		/// Constructs a new context instance
		/// </summary>
		/// <param name="lazyLoading">Indicating whether lazy loading of relationships exposed as navigation properties is enabled.</param>
		/// <param name="proxyCreation">
		/// Indicating whether or not the framework will create instances of dynamically generated proxy classes whenever it creates an instance of an entity type.
		/// Note that disabling proxy creation will also disable lazy loading.
		/// </param>
		public ILBContext(bool lazyLoading = true, bool proxyCreation = true)
			: this()
		{
			this.Configuration.LazyLoadingEnabled = lazyLoading;
			this.Configuration.ProxyCreationEnabled = proxyCreation;
		}

		/// <summary>
		/// Constructs a new context instance
		/// </summary>
		/// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
		/// <param name="lazyLoading">Indicating whether lazy loading of relationships exposed as navigation properties is enabled.</param>
		/// <param name="proxyCreation">
		/// Indicating whether or not the framework will create instances of dynamically generated proxy classes whenever it creates an instance of an entity type.
		/// Note that disabling proxy creation will also disable lazy loading.
		/// </param>
		public ILBContext(string nameOrConnectionString, bool lazyLoading = true, bool proxyCreation = true)
			: this(nameOrConnectionString)
		{
			this.Configuration.LazyLoadingEnabled = lazyLoading;
			this.Configuration.ProxyCreationEnabled = proxyCreation;
		}


		public DbSet<mail> mail { get; set; }
		public DbSet<users> users { get; set; }

		/// <summary>
		/// Gets the object context.
		/// </summary>
		public ObjectContext ObjectContext
		{
			get
			{
				return ((IObjectContextAdapter)this).ObjectContext;
			}
		}

		/// <summary>
		/// Gets the object state manager used by the object context to track object changes.
		/// </summary>
		public ObjectStateManager ObjectStateManager
		{
			get
			{
				return ObjectContext.ObjectStateManager;
			}
		}

		/// <summary>
		/// Changes state of the System.Data.Objects.ObjectStateEntry for a specific object to the specified entityState.
		/// </summary>
		public ObjectStateEntry ChangeObjectState(ILBBaseEntity entity, EntityState entityState)
		{
			return ObjectStateManager.ChangeObjectState(entity, entityState);
		}

		/// <summary>
		/// Custom model creating
		/// </summary>
		partial void CustomModelCreating(DbModelBuilder modelBuilder);

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Tell Code First to ignore PluralizingTableName convention
			// If you keep this convention then the generated tables will have pluralized names.
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			// Comment this line if you want to use EdmMetadata table to track your datatbase changes
			// more info: http://weblogs.asp.net/shijuvarghese/archive/2011/10/06/removing-edmmetadata-table-in-ef-code-first.aspx
			// EdmMetadata table keeps hash of current code-first model and it allows DbContext
			// detecting changes of model so that database can be recreated. This feature is turned on
			// by default, which i have disabled it here.
			modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

			CustomModelCreating(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}
	}
}
