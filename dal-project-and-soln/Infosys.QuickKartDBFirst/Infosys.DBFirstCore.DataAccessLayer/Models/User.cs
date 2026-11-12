using System;
using System.Collections.Generic;

namespace Infosys.DBFirstCore.DataAccessLayer.Models;

public partial class User
{
    public string EmailId { get; set; }

    public string UserPassword { get; set; }

    public byte? RoleId { get; set; }

    public string Gender { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Address { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual Role Role { get; set; }
}
