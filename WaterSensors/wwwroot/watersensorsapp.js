(function () {
    const waterSensorMapCanvas = "water_sensor_map_canvas";
    const canvas = document.getElementById(waterSensorMapCanvas);
    if (canvas && canvas.getContext) {
        renderMap();
    }
    else
    {
        console.log(`Expected ${waterSensorMapCanvas} canvas element not detected.`);
    }

    function renderMap() {
        const ctx = canvas.getContext("2d");
        console.log('got context', ctx);
    }
})();

