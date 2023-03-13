using System;

namespace Pools
{
	public interface IPoolObject
	{
		void BindReleaseHandle(IDisposable releaseHandle);
		void Release();
	}
}
