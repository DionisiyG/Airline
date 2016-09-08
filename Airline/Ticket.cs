using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    enum TicketType : byte
    {
        Business,
        Econom
    }

    struct Ticket
    {
        public TicketType ClassType;
        public decimal Price;

        public Ticket(TicketType classType, decimal price)
        {
            ClassType = classType;
            Price = price;
        }
        public override string ToString()
        {
            string type = ClassType.ToString();
            return string.Format("Type: {0} Price: {1}", type[0], Price.ToString("C", new CultureInfo("en-US")));
        }
    }
}
