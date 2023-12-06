using Serilog.Core;
using Serilog.Sinks.Syslog;
using Serilog;
using System.Text;
using System;

namespace TestRSyslog
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            SyslogTcpConfig tcpConfig = new()
            {
                Host = "10.200.222.5",
                Port = 6514,
                Formatter = new Rfc5424Formatter(),
                Framer = new MessageFramer(FramingType.OCTET_COUNTING, Encoding.UTF8),
                UseTls = true,
                CertValidationCallback = (sender, certificate, chain, errors) => true,
                CertProvider = new CertificateFileProvider("C:\\cert.pfx"),
            };

            Logger log = new LoggerConfiguration().WriteTo.TcpSyslog(tcpConfig)
                .Enrich.FromLogContext()
                .CreateLogger();

            log.Information("Test message");

            await Task.Delay(5000);
        }
    }
}
