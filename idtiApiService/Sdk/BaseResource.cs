using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace idtiApiService.Sdk
{
    public class BaseResource : IDisposable
    {
        private bool _alreadyDisposed = false;

        // finalizer
        // virtual Dispose ¸Å¼­µå¸¦ È£ÃâÇÑ´Ù.
        ~BaseResource() {
            Dispose(false);
        }

        // IDisposable interface ÀÇ ±¸Çö
        // virtual Dispose ¸Å¼­µå¸¦ È£ÃâÇÏ°í
        // FinalizationÀÌ ¼öÇàµÇÁö ¾Êµµ·Ï ÇÑ´Ù.
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        // virtual Dispose ¸Þ¼­µå
        protected virtual void Dispose(bool isDisposing) {
            // ¿©·¯¹ø dispose¸¦ ¼öÇàÇÏÁö ¾Êµµ·Ï ÇÑ´Ù. 
            if (_alreadyDisposed)
                return;

            if (isDisposing) {
                // ÇØ¾ßÇÒ ÀÏ : managed ¸®¼Ò½º¸¦ ÇØÁ¦ÇÑ´Ù.
                // ÇØ¾ßÇÒ ÀÏ : unmanaged ¸®¼Ò½º¸¦ ÇØÁ¦ÇÑ´Ù.
                // disposed ÇÃ·¡±×¸¦ ¼³Á¤ÇÑ´Ù.

                _alreadyDisposed = true;
            }
        }

        // Allow your Dispose method to be called multiple times,
        // but throw an exception if the object has been disposed.
        // Whenever you do something with this class, 
        // check to see if it has been disposed.
        public void IsDisposing() {
            if (this._alreadyDisposed) {
                throw new ObjectDisposedException("already Disposed!");
            }
        }


        /// ////////////////////////////////////////////
    }
}
