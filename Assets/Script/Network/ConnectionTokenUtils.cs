using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ConnectionTokenUtils
{
    //主要功能：在每個玩家升上處理Token，以便透過一些代碼實現辨識玩家存在與否，避免在host轉移時，遇到重複生成或是覆蓋的問題
    public static byte[] NewToken() => Guid.NewGuid().ToByteArray();

    public static int HashToken(byte[] token) => new Guid(token).GetHashCode();

    public static string TokenToString(byte[] token) => new Guid(token).ToString();

}
