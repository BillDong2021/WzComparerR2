﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WzComparerR2.Network.Contracts
{
    [JsonObject("14")]
    sealed class PackOnUserUpdate
    {
        public UserUpdateReason UpdateReason { get; set; }
        public UserInfo UserInfo { get; set; }
    }

    enum UserUpdateReason
    {
        InfoChanged = 0,
        Online = 1,
        Offline = 2
    }
}
