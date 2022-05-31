using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using log4net;

namespace AccessoriesPlus.Projectiles
{
	public class WebSlingerHook : ModProjectile
	{
		private static Asset<Texture2D> chainTexture;

		public override void Load()
		{
			chainTexture = ModContent.Request<Texture2D>("AccessoriesPlus/Projectiles/WebSlingerHook_Chain");
		}

		public override void Unload()
		{
			chainTexture = null;
		}


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Web Slinger Hook");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
			Projectile.width = 12;
			Projectile.height = 16;
		}


		// AI
		Vector2 playerVelocity = new(0f, 0f);

		public override void AI()
        {
			// PreAI stuff
			Player player = Main.player[Projectile.owner];

			if (player.dead)
				Projectile.Kill();

			// Distance and direction to player
			Vector2 dirToPlayer = Projectile.DirectionTo(player.Center);
			float dstToPlayer = MathF.Sqrt(MathF.Pow(Projectile.Center.X - player.Center.X, 2f) + MathF.Pow(Projectile.Center.Y - player.Center.Y, 2f));

			// Setting playerVelocity to player.velocity so movement is smooth
			if (Projectile.timeLeft == 3600 * 10)
            {
				playerVelocity = player.velocity;
			}

			// Gravity
			playerVelocity += new Vector2(0f, player.gravity * player.gravDir);

			// Movement speed caps
			if (playerVelocity.Y > player.maxFallSpeed)
				playerVelocity.Y = player.maxFallSpeed;

			if (playerVelocity.Y < -player.maxFallSpeed)
				playerVelocity.Y = -player.maxFallSpeed;

			if (playerVelocity.X > player.maxRunSpeed)
				playerVelocity.X = player.maxRunSpeed;

			if (playerVelocity.X < -player.maxRunSpeed)
				playerVelocity.X = -player.maxRunSpeed;

			/*/ Stopping velocity from changing if the player isn't moving TODO make it stop the player when they are at a standstill with velocity, not a standstill with no 
			if (player.position.Y == player.oldPosition.Y && player.velocity.Y != 0f)
				playerVelocity.Y = 0f;

			if (player.position.X == player.oldPosition.X && player.velocity.X != 0f)
				playerVelocity.X = 0f;*/
		}


		// Returns true if the player can use the grappling hook
        public override bool? CanUseGrapple(Player player)
		{
			int hooksOut = 0;
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type)
				{
					hooksOut++;
				}
			}

			return hooksOut < 2;
		}

        // Kills the oldest hook when a new one is shot out
		public override void UseGrapple(Player player, ref int type)
		{
			int hooksOut = 0;
			int oldestHookIndex = -1;
			int oldestHookTimeLeft = 100000;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == Projectile.whoAmI && Main.projectile[i].type == Projectile.type)
				{
					hooksOut++;
					if (Main.projectile[i].timeLeft < oldestHookTimeLeft)
					{
						oldestHookIndex = i;
						oldestHookTimeLeft = Main.projectile[i].timeLeft;
					}
				}
			}
			if (hooksOut > 1)
			{
				Main.projectile[oldestHookIndex].Kill();
			}
		}

        // Adjusts the range of the hook, Amethyst Hook is 300, Static Hook is 600.
        public override float GrappleRange() { return 500f; }

		// Adjusts how many hooks the grappling hook has
		public override void NumGrappleHooks(Player player, ref int numHooks) { numHooks = 1; }

		// Adjusts how fast the grapple returns to you after meeting its max shoot distance, default is 11, Lunar is 24
		public override void GrappleRetreatSpeed(Player player, ref float speed) { speed = 20f; }

		// Adjusts how fast you get pulled to the grappling hook projectile's landing position
		public override void GrapplePullSpeed(Player player, ref float speed) { speed = 20f; }

		// Adjusts the position the player is when grappled
		public override void GrappleTargetPoint(Player player, ref float grappleX, ref float grappleY)
		{
			// Distance and direction to player
			Vector2 dirToPlayer = Projectile.DirectionTo(player.Center);
			float dstToPlayer = MathF.Sqrt(MathF.Pow(Projectile.Center.X - player.Center.X, 2f) + MathF.Pow(Projectile.Center.Y - player.Center.Y, 2f));

			// Making the player hang in the same position
			float hangDist = dstToPlayer;
			grappleX += dirToPlayer.X * hangDist;
			grappleY += dirToPlayer.Y * hangDist;

			// Adding velocity to stop the player from going too far from the hook TODO: make it good ----------------------------------
			// This is in GrappleTargetPoint() instead of AI() because it would make the player jerk forward when the hook reaches the furthest distance from the player
			if (dstToPlayer > GrappleRange())
			{
				playerVelocity += (dstToPlayer - GrappleRange()) * -dirToPlayer;
			}

			// Moving the player from playerVelocity, which is used in AI()
			grappleX += playerVelocity.X;
			grappleY += playerVelocity.Y;
		}

		// Draws the grappling hook's chain
		public override bool PreDrawExtras()
		{
			Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
			Vector2 center = Projectile.Center;
			Vector2 directionToPlayer = playerCenter - Projectile.Center;
			float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
			float distanceToPlayer = directionToPlayer.Length();

			while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
			{
				directionToPlayer /= distanceToPlayer;
				directionToPlayer *= chainTexture.Height();

				center += directionToPlayer;
				directionToPlayer = playerCenter - center;
				distanceToPlayer = directionToPlayer.Length();

				Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

				// Draw chain
				Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
					chainTexture.Value.Bounds, drawColor, chainRotation,
					chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
			}
			// Stop vanilla from drawing the default chain.
			return false;
		}

		#region Old Code
		/*
		private static Asset<Texture2D> chainTexture;

		public override void Load()
        {
			chainTexture = ModContent.Request<Texture2D>("AccessoriesPlus/Projectiles/WebSlingerHook_Chain");

		}

        public override void Unload()
        {
			chainTexture = null;
		}

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Web Slinger Hook");
			Main.projHook[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3600 * 10;
			Projectile.aiStyle = 7;
		}

        
		float range = 500f;
		float reelSpeed = 18f;
		bool reelingIn = false;
		bool hooked = false;

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			// Movement
			Vector2 dirToPlayer = Projectile.DirectionTo(player.Center);
			float dstToPlayer = MathF.Sqrt(MathF.Pow(player.Center.X - Projectile.Center.X, 2) + MathF.Pow(player.Center.Y - Projectile.Center.Y, 2));
			bool isTouchingPlayer = dstToPlayer < 10f;
			bool shouldReelIn = dstToPlayer > range;

			// Facing direction of movement
			if (reelingIn)
			{
				Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
			}
			else if (hooked)
            {
				Projectile.rotation = dirToPlayer.ToRotation() - MathHelper.PiOver2;
			}
			else
            {
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			}


			if (player.dead || player.stoned || player.webbed || player.frozen)
			{
				Projectile.Kill();
            }

			if ((shouldReelIn || reelingIn) && !hooked)
            {
				Projectile.velocity = dirToPlayer * reelSpeed;
				reelingIn = true;
            }

			if (isTouchingPlayer && reelingIn)
            {
				Projectile.Kill();
            }

			// Hooking onto blocks TODO block player movement while hooked
			Point posInTileCoords = Projectile.Center.ToTileCoordinates();
			Tile tileAtPos = Main.tile[posInTileCoords.X, posInTileCoords.Y];
			bool shouldHookToTile = tileAtPos.HasUnactuatedTile && (Main.tileSolid[tileAtPos.TileType] || (tileAtPos.TileType == 314) || (TileID.Sets.IsATreeTrunk[tileAtPos.TileType]) || (tileAtPos.TileType == 323));

			if (shouldHookToTile && !reelingIn)
            {
				hooked = true;
            }

			if (hooked)
            {
				Projectile.velocity = Vector2.Zero;
				Projectile.Center = posInTileCoords.ToWorldCoordinates();

				// Moving the player TODO make it good
				if (dstToPlayer > range)
                {
					player.position += (dstToPlayer - range) * -dirToPlayer;
                }
			}

			// TODO make it destroy when player uses magic mirror / rod of discord / pylon + make oldest hook destroy when a new one attaches
			bool shouldDestroy = (Main.keyState.IsKeyDown(Keys.Space) && !Main.oldKeyState.IsKeyDown(Keys.Space)) || player.teleporting;
			if (hooked && shouldDestroy)
            {
				Projectile.Kill();
            }
        }
		

        // Returns true if the player can fire another grappling hook
        public override bool? CanUseGrapple(Player player)
		{
			int hooksOut = 0;
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type)
				{
					hooksOut++;
				}
			}
			if (hooksOut > 1)
			{
				return false;
			}
			return true;
		}

		// Draws the web to the hook TODO: Fix misalignment at the projectile
		public override bool PreDrawExtras()
		{
			Vector2 playerCenter = Main.player[Projectile.owner].Center;
			Vector2 center = Projectile.Center;
			Vector2 directionToPlayer = playerCenter - Projectile.Center;
			float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
			float distanceToPlayer = directionToPlayer.Length();

			while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
			{
				directionToPlayer /= distanceToPlayer;
				directionToPlayer *= chainTexture.Height();

				center += directionToPlayer;
				directionToPlayer = playerCenter - center;
				distanceToPlayer = directionToPlayer.Length();

				Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

				// Draw chain
				Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
					chainTexture.Value.Bounds, drawColor, chainRotation,
					chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
			}
			// Stop vanilla from drawing the default chain.
			return false;
		}
		*/
		#endregion
	}
}