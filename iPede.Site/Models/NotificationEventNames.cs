using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models
{
    public static class NotificationEventNames
    {
        public static readonly string
            OrderCreated = "ORDER_CREATED",
            OrderItemCreated = "ORDER_ITEM_CREATED";
    }
}