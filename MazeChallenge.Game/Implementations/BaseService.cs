using System;
using MazeChallenge.Persistence.UnitOfWork;

namespace MazeChallenge.Game.Implementations
{
	public abstract class BaseService
	{
		private IUnitOfWork _unitOfWork;

		public BaseService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
        }
	}
}

