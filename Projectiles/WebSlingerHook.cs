using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using log4net;
using Terraria.DataStructures;

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
		public override void AI()
        {
			// PreAI stuff
			Player player = Main.player[Projectile.owner];

			if (player.dead)
				Projectile.Kill();
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


		// Where all the AI code is located // TODO: make the moving down more precise, make direction change based on velocity, allow it to grapple to trees, and make grapple range adjusting at the end of ai smoother
		static Vector2 playerVelocity = new(float.PositiveInfinity, float.PositiveInfinity);

		float dstToPlayerWhenGrappled = 500f;

		bool swinging = false;
		float swingDirection = float.PositiveInfinity;

		const float swingSpeed = 0.4f;
		const float swingDirChangeThreshold = 300f;

		const float stopSpeedThreshold = 0.3f;
		const float playerSlowdownSpeed = 0.02f;

		public override void GrappleTargetPoint(Player player, ref float grappleX, ref float grappleY)
		{
			// Smooth transition into hooking and sets the variable grapple distance
			bool justHooked = playerVelocity == new Vector2(float.PositiveInfinity, float.PositiveInfinity);
			if (justHooked)
			{
				// Smoothing tranistion
				if (player.velocity != Vector2.Zero)
				{
					playerVelocity = player.velocity;
				}

				// Variable grapple distance
				dstToPlayerWhenGrappled = DistanceBetweenPoints(Projectile.Center, player.Center);
			}

			// Making the player hang in the same position
			grappleX = player.Center.X;
			grappleY = player.Center.Y;

			// Distance and direction to player
			Vector2 dirToPlayer = Projectile.DirectionTo(player.Center);
			float dstToPlayer = DistanceBetweenPoints(Projectile.Center, player.Center);


			// If the player is swinging
			if (dstToPlayer > dstToPlayerWhenGrappled || dstToPlayer > GrappleRange())
				swinging = true;
			else
				swinging = false;


			// Gravity
			playerVelocity += new Vector2(0f, player.gravity * player.gravDir);

			// Swinging
			if (swinging)
			{
				// Resetting gravity
				playerVelocity.Y = 0f;

				// Setting swing direction
				float xDifference = Projectile.Center.X - player.Center.X;
				bool shouldChangeDir = MathF.Abs(xDifference) > swingDirChangeThreshold;
				if (shouldChangeDir || swingDirection == float.PositiveInfinity)
					swingDirection = xDifference / MathF.Abs(xDifference);

				// Changing the x velocity of the player to make them swing
				if (!float.IsNaN(swingDirection))
					playerVelocity.X += swingSpeed * swingDirection;

				// Moving the player down so they are at the edge of the swing distance
				playerVelocity.Y += 25f * player.gravDir;
			}


			// Slowing down the player
			if (playerVelocity.X > 0f)
				playerVelocity.X -= playerSlowdownSpeed;

			if (playerVelocity.X < 0f)
				playerVelocity.X += playerSlowdownSpeed;

			if (playerVelocity.X < stopSpeedThreshold && playerVelocity.X > -stopSpeedThreshold)
				playerVelocity.X = 0f;
			 

			// Movement speed caps
			if (playerVelocity.Y > player.maxFallSpeed)
				playerVelocity.Y = player.maxFallSpeed;

			if (playerVelocity.Y < -player.maxFallSpeed)
				playerVelocity.Y = -player.maxFallSpeed;

			if (playerVelocity.X > player.maxFallSpeed)
				playerVelocity.X = player.maxFallSpeed;

			if (playerVelocity.X < -player.maxFallSpeed)
				playerVelocity.X = -player.maxFallSpeed;


			// Moving the player
			grappleX += playerVelocity.X;
			grappleY += playerVelocity.Y;

			// Moving the player closer to the hook if they are too far away
			float newDstToPlayer = DistanceBetweenPoints(Projectile.Center, new Vector2(grappleX, grappleY));
			if (newDstToPlayer > GrappleRange() || newDstToPlayer > dstToPlayerWhenGrappled)
			{
				Vector2 distanceToMove = (newDstToPlayer - dstToPlayerWhenGrappled) * -dirToPlayer;
				grappleX += distanceToMove.X;
				grappleY += distanceToMove.Y;
			}
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

		// Calculates the distance between 2 points
		static float DistanceBetweenPoints(Vector2 pointA, Vector2 pointB)
        {
			return MathF.Sqrt(MathF.Pow(pointA.X - pointB.X, 2f) + MathF.Pow(pointA.Y - pointB.Y, 2f));
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