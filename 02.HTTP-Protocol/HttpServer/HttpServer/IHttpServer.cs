using System;
using System.Collections.Generic;
using System.Text;

namespace HttpServer
{
    public interface IHttpServer
    {
        void Start();

        void Stop();
    }
}
