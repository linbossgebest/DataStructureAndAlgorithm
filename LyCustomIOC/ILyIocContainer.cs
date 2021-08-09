using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyCustomIOC
{
    public interface ILyIocContainer
    {
        void Register<TFrom, TTo>(string nickName = null, object[] paramList = null, LifeTimeType lifeTime = LifeTimeType.Transient) where TTo : TFrom;

        TFrom Resolve<TFrom>(string nickName = null);

        ILyIocContainer CreateChildContainer();
    }
}
