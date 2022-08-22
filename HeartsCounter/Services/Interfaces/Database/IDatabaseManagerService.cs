using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Services.Interfaces.Database
{
    public interface IDatabaseManagerService
    {
        SQLiteConnection SQLConnetion { get; }
    }
}
