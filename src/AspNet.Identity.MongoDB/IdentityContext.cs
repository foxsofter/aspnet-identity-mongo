﻿namespace AspNet.Identity.MongoDB
{
	using global::MongoDB.Driver;
	using global::MongoDB.Driver.Builders;

	public class IdentityContext
	{
		public MongoCollection Users { get; set; }
		public MongoCollection Roles { get; set; }

		public IdentityContext(MongoCollection users)
		{
			Users = users;
			EnsureUniqueIndexOnUserName(users);
		}

		public IdentityContext(MongoCollection users, MongoCollection roles) : this(users)
		{
			Roles = roles;
			EnsureUniqueIndexOnRoleName(roles);
		}

		private void EnsureUniqueIndexOnUserName(MongoCollection users)
		{
			var userName = new IndexKeysBuilder<IdentityUser>().Ascending(t => t.UserName);
			var unique = new IndexOptionsBuilder().SetUnique(true);
			users.CreateIndex(userName, unique);
		}

		private void EnsureUniqueIndexOnRoleName(MongoCollection roles)
		{
			var roleName = new IndexKeysBuilder<IdentityRole>().Ascending(t => t.Name);
			var unique = new IndexOptionsBuilder().SetUnique(true);
			roles.CreateIndex(roleName, unique);
		}

		public void EnsureUniqueIndexOnEmail()
		{
			// note: I'm not making the index on email required, I'd like to start a conversation around how to ensure indexes
			var email = new IndexKeysBuilder<IdentityUser>().Ascending(t => t.Email);
			var unique = new IndexOptionsBuilder().SetUnique(true);
			Users.CreateIndex(email, unique);
		}
	}
}