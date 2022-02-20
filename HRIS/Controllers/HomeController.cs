using DPCtlUruNet;
using DPUruNet;
using HRIS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace HRIS.Controllers
{
    public class HomeController : Controller
    {
        private ReaderCollection _readers;
        private DPCtlUruNet.EnrollmentControl _enrollmentControl;

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var username = _userManager.GetUserName(User);
            try
            {
                _readers = ReaderCollection.GetReaders();
                foreach (Reader Reader in _readers)
                {
                    var val = Reader.Description.Name;
                }
            }
            catch (Exception ex)
            {
                var message = "Cannot access readers";
            }
            return View();
        }

        private void Enroll()
        {
            //if (_enrollmentControl != null)
            //{
            //    _enrollmentControl.Reader = _sender.CurrentReader;
            //}
            //else
            //{
            //    _enrollmentControl = new DPCtlUruNet.EnrollmentControl(_sender.CurrentReader, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
            //    _enrollmentControl.BackColor = System.Drawing.SystemColors.Window;
            //    _enrollmentControl.Location = new System.Drawing.Point(3, 3);
            //    _enrollmentControl.Name = "ctlEnrollmentControl";
            //    _enrollmentControl.Size = new System.Drawing.Size(482, 346);
            //    _enrollmentControl.TabIndex = 0;
            //    _enrollmentControl.OnCancel += new DPCtlUruNet.EnrollmentControl.CancelEnrollment(this.enrollment_OnCancel);
            //    _enrollmentControl.OnCaptured += new DPCtlUruNet.EnrollmentControl.FingerprintCaptured(this.enrollment_OnCaptured);
            //    _enrollmentControl.OnDelete += new DPCtlUruNet.EnrollmentControl.DeleteEnrollment(this.enrollment_OnDelete);
            //    _enrollmentControl.OnEnroll += new DPCtlUruNet.EnrollmentControl.FinishEnrollment(this.enrollment_OnEnroll);
            //    _enrollmentControl.OnStartEnroll += new DPCtlUruNet.EnrollmentControl.StartEnrollment(this.enrollment_OnStartEnroll);
            //}
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
