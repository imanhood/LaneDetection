namespace Presentaion.Models {
    public class ImageProcessingModel {
        public string picture { get; set; }
        public string outputPicture => !string.IsNullOrWhiteSpace(this.picture) && this.picture.EndsWith(".jpg") ? this.picture.Split(".jpg")[0] + "_output.jpg?v=" + DateTime.Now.Ticks.ToString() : "";
        public string thresholdedPicture => !string.IsNullOrWhiteSpace(this.picture) && this.picture.EndsWith(".jpg") ? this.picture.Split(".jpg")[0] + "_thresholded.jpg?v=" + DateTime.Now.Ticks.ToString() : "";
        public int topLeftX { get; set; } = 45;
        public int topLeftY { get; set; } = 55;
        public int topRightX { get; set; } = 65;
        public int topRightY { get; set; } = 55;
        public int bottomLeftX { get; set; } = -7;
        public int bottomLeftY { get; set; } = 100;
        public int bottomRightX { get; set; } = 100;
        public int bottomRightY { get; set; } = 100;
        public string corners => $"{bottomLeftX / 100.0}, {(100 - bottomRightX) / 100.0}, {topLeftX / 100.0}, {(100 - topRightX) / 100.0}, {(100 - bottomLeftY) / 100.0}, {(100 - bottomRightY) / 100.0}, {topLeftY / 100.0}, {topRightY / 100.0}";
    }
}


//-0.07, 0.15, 0.47, 0.3, 0, 0, 0.55, 0.55