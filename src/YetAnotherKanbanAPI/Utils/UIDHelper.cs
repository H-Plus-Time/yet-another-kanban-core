using System;

namespace YetAnotherKanbanAPI.Utils
{
    public static class shortUid
    {
      public static string generate() {
        string b64string = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        // Not great in terms of performance, but fit for purpose.
        return b64string.Replace("-", "").Replace("/", "").Replace("+", "").Replace("=", "");
      }
    }
}
