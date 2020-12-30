using br.com.galdino.mocroservice.two.domain.core.Interface;
using br.com.galdino.mocroservice.two.domain.core.Service;
using br.com.galdino.mocroservice.two.domain.crosscouting.Entity;
using br.com.galdino.mocroservice.two.domain.crosscouting.Entity.Response;
using br.com.galdino.mocroservice.two.domain.crosscouting.Enum;
using br.com.galdino.mocroservice.two.domain.crosscouting.Infrastrscture;
using br.com.galdino.mocroservice.two.utils.Interface.Request;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace br.com.galdino.mocroservice.two.test.CalculateMeters
{
    public class CalculateMetersAndConsultingService
    {

        private readonly Mock<ICalculateSquareValueService> mockServiceSquareMeter = new Mock<ICalculateSquareValueService>();
        private readonly Mock<IWebRequestService> mockWebRequestService = new Mock<IWebRequestService>();
        private readonly Mock<InfrastructureConfig> mockInfraConfig = new Mock<InfrastructureConfig>();

        private CalculateSquareValueService calculateMetersService() =>
            new CalculateSquareValueService(mockWebRequestService.Object, new InfrastructureConfig
            {
                MicroServices = new MicroServicesConfig
                {
                    SquareHost = "https://localhost:5001",
                    Url = "/SquareMeter"
                }
            });

        [Theory]
        [InlineData(125.99)]
        public async Task CalculateMetersSendParametterForService(decimal meters)
        {
            mockWebRequestService
                .Setup(c => c.RequestJsonSerialize<ResponseDataService>("",null,ETypeMethod.GET,null,null,null)).ReturnsAsync(It.IsAny<ResponseDataService>());

            mockServiceSquareMeter
                .Setup(c => c.Post(meters)).ReturnsAsync(It.IsAny<CalculateModelView>());

            var send = calculateMetersService();
            var result = await send.Post(meters);

            Assert.NotNull(result);
        }
    }
}
