using System;
using MazeChallenge.Persistence.UnitOfWork;

namespace MazeChallenge.Game.Implementations
{
	public abstract class BaseService
	{
		protected IUnitOfWork _unitOfWork;

		public BaseService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
        }
	}
}

