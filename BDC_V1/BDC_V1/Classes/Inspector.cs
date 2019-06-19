using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class Inspector : Person, IInspector
    {
        /// <inheritdoc />
        public Guid? UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }
        private Guid? _userId;

        /// <inheritdoc />
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        private string _userName;

        /// <inheritdoc />
        public DateTime PasswordChanged
        {
            get => _passWordChanged;
            set => SetProperty(ref _passWordChanged, value);
        }
        private DateTime _passWordChanged;

        /// <inheritdoc />
        public string Password
        {
            get => _passWord;
            set => SetProperty(ref _passWord, value);
        }
        private string _passWord;

        /// <inheritdoc />
        public bool Disabled
        {
            get => _disabled;
            set => SetProperty(ref _disabled, value);
        }
        private bool _disabled;

        public int CompareTo(IInspector other)
        {
            if (UserId.HasValue && other.UserId.HasValue)
                return UserId.Value.CompareTo(other.UserId.Value);

            return base.CompareTo(other);
        }

        [NotNull]
        public static string HashPassword([CanBeNull] string password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;

            // TODO: If needed, get the hash algorithm
            return password;
        }

    }
}
