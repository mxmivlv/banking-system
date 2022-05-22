using System;

namespace Module_14.Models
{
    internal class CodeException : Exception
    {
        public int Code { get { return code; } }

        private int code;

        /// <summary>
        /// Конструктор класса ошибок
        /// </summary>
        /// <param name="msg">Название ошибки</param>
        /// <param name="code">Код ошибки</param>
        public CodeException(string msg, int code) : base(msg)
        {
            this.code = code;
        }
    }
}
