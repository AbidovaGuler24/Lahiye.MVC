using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.Core.ViewModels
{
    public class PaidBookDetailViewModel
    {
        public PaidBookVm PaidBook { get; set; }
        public List<PaidBookVm> RecommendedPaidBooks { get; set; } = new();
    }
}
