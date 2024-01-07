namespace Presentaion.Models {
    public class ImageProcessingModel {
        public string picture { get; set; }
        public int topLeftX { get; set; } = 30;
        public int topLeftY { get; set; } = 55;
        public int topRightX { get; set; } = 70;
        public int topRightY { get; set; } = 55;
        public int bottomLeftX { get; set; } = 0;
        public int bottomLeftY { get; set; } = 100;
        public int bottomRightX { get; set; } = 100;
        public int bottomRightY { get; set; } = 100;
    }
}
