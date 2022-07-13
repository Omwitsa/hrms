using DPUruNet;
using HRIS.IProviders;
using HRIS.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HRIS.Providers
{
    public class ScannerProvider : IScannerProvider
    {
        private ReaderCollection _readers;
        List<Fmd> preenrollmentFmds;
        int count;

        public bool Reset
        {
            get { return reset; }
            set { reset = value; }
        }
        private bool reset;

        public ScannerProvider()
        {
        }

        // When set by child forms, shows s/n and enables buttons.
        public Reader CurrentReader
        {
            get { return currentReader; }
            set
            {
                currentReader = value;
                //SendMessage(Action.UpdateReaderState, value);
            }
        }
        private Reader currentReader;

        public ReturnData<List<string>> GetReaders()
        {
            try
            {
                _readers = ReaderCollection.GetReaders();
                var readerNames = new List<string>();
                foreach (Reader Reader in _readers)
                {
                    readerNames.Add(Reader.Description.Name);
                }
                
                return new ReturnData<List<string>>
                {
                    Success = true,
                    Data = readerNames
                };
            }
            catch (Exception ex)
            {
                return new ReturnData<List<string>>
                {
                    Success = false,
                    Message = "Cannot access readers"
                };
            }
        }

        public ReturnData<string> Enroll()
        {
            try
            {
                count = 0;
                preenrollmentFmds = new List<Fmd>();
                GetCurrentReader();
                //SendMessage(Action.SendMessage, "Place a finger on the reader.");
                if (!OpenReader())
                {
                    //this.Close();
                }
                if (!StartCaptureAsync(this.OnCaptured))
                {
                    //this.Close();
                }
                return new ReturnData<string>
                {
                    Success = true,
                    Message = "Enrolled Successfully"
                };
            }
            catch (Exception)
            {
                return new ReturnData<string>
                {
                    Success = false,
                    Message = ""
                };
            }
        }

        private void GetCurrentReader()
        {
            _readers = ReaderCollection.GetReaders();
            if (CurrentReader != null)
            {
                CurrentReader.Dispose();
                CurrentReader = null;
            }
            
            CurrentReader = _readers[0];
            //_readers.Dispose();
        }

        private bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {
            // Activate capture handler
            currentReader.On_Captured += new Reader.CaptureCallback(OnCaptured);

            // Call capture
            if (!CaptureFingerAsync())
            {
                return false;
            }

            return true;
        }

        private bool CaptureFingerAsync()
        {
            try
            {
                GetStatus();

                DPUruNet.Constants.ResultCode captureResult = currentReader.CaptureAsync(DPUruNet.Constants.Formats.Fid.ANSI, DPUruNet.Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, currentReader.Capabilities.Resolutions[0]);
                if (captureResult != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                {
                    reset = true;
                    throw new Exception("" + captureResult);
                }

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:  " + ex.Message);
                return false;
            }
        }

        private void GetStatus()
        {
            DPUruNet.Constants.ResultCode result = currentReader.GetStatus();

            if ((result != DPUruNet.Constants.ResultCode.DP_SUCCESS))
            {
                reset = true;
                throw new Exception("" + result);
            }

            if ((currentReader.Status.Status == DPUruNet.Constants.ReaderStatuses.DP_STATUS_BUSY))
            {
                Thread.Sleep(50);
            }
            else if ((currentReader.Status.Status == DPUruNet.Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
            {
                currentReader.Calibrate();
            }
            else if ((currentReader.Status.Status != DPUruNet.Constants.ReaderStatuses.DP_STATUS_READY))
            {
                throw new Exception("Reader Status - " + currentReader.Status.Status);
            }
        }

        private void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                // Check capture quality and throw an error if bad.
                if (!CheckCaptureResult(captureResult)) return;

                count++;

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, DPUruNet.Constants.Formats.Fmd.ANSI);
                //SendMessage(Action.SendMessage, "A finger was captured.  \r\nCount:  " + (count));

                if (resultConversion.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                {
                    //_sender.Reset = true;
                    //throw new Exception(resultConversion.ResultCode.ToString());
                }

                preenrollmentFmds.Add(resultConversion.Data);

                if (count >= 4)
                {
                    DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(DPUruNet.Constants.Formats.Fmd.ANSI, preenrollmentFmds);

                    if (resultEnrollment.ResultCode == DPUruNet.Constants.ResultCode.DP_SUCCESS)
                    {
                        //SendMessage(Action.SendMessage, "An enrollment FMD was successfully created.");
                        //SendMessage(Action.SendMessage, "Place a finger on the reader.");
                        preenrollmentFmds.Clear();
                        count = 0;
                        return;
                    }
                    else if (resultEnrollment.ResultCode == DPUruNet.Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                        //SendMessage(Action.SendMessage, "Enrollment was unsuccessful.  Please try again.");
                        //SendMessage(Action.SendMessage, "Place a finger on the reader.");
                        preenrollmentFmds.Clear();
                        count = 0;
                        return;
                    }
                }

                //SendMessage(Action.SendMessage, "Now place the same finger on the reader.");
            }
            catch (Exception ex)
            {
                // Send error message, then close form
                //SendMessage(Action.SendMessage, "Error:  " + ex.Message);
            }
        }

        private bool OpenReader()
        {
            reset = false;
            //DPUruNet.Constants.ResultCode result = DPUruNet.Constants.ResultCode.DP_DEVICE_FAILURE;
            // Open reader
            DPUruNet.Constants.ResultCode result = currentReader.Open(DPUruNet.Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
            if (result != DPUruNet.Constants.ResultCode.DP_SUCCESS)
            {
                //MessageBox.Show("Error:  " + result);
                reset = true;
                return false;
            }

            return true;
        }

        private bool CheckCaptureResult(object captureResult)
        {
            using (Tracer tracer = new Tracer("Form_Main::CheckCaptureResult"))
            {
                //if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                //{
                //    if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                //    {
                //        reset = true;
                //        throw new Exception(captureResult.ResultCode.ToString());
                //    }

                //    // Send message if quality shows fake finger
                //    if ((captureResult.Quality != Constants.CaptureQuality.DP_QUALITY_CANCELED))
                //    {
                //        throw new Exception("Quality - " + captureResult.Quality);
                //    }
                //    return false;
                //}

                return true;
            }
        }
    }
}
