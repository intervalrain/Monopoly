﻿using System.Runtime.Serialization;
namespace Shared.Domain.Exceptions
{
	public class PlayerNeedToChooseDirectionException : Exception
	{
		public PlayerNeedToChooseDirectionException()
		{
		}

		public PlayerNeedToChooseDirectionException(string? message)
			: base(message)
		{
		}

		public PlayerNeedToChooseDirectionException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}

		public PlayerNeedToChooseDirectionException(Player player, Block currentBlock, List<Direction> directions)
		{
			// TODO: write message
		}

		protected PlayerNeedToChooseDirectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
    }
}
