let preview = null;
let mediaRecorder = null;
let isRecording = false;

function startRecording() {
    preview = document.getElementById("preview");

    navigator.mediaDevices
        .getDisplayMedia({
            video: true,
            audio: true,
        })
        .then((stream) => {
            preview.srcObject = stream;
            preview.captureStream = preview.captureStream || preview.mozCaptureStream;

            return new Promise((resolve) => (preview.onplaying = resolve));
        })
        .then(() => {
            mediaRecorder = new MediaRecorder(preview.captureStream());
            const recordedChunks = [];

            mediaRecorder.ondataavailable = (event) => {
                if (event.data.size > 0) {
                    recordedChunks.push(event.data);
                }
            };

            mediaRecorder.onstop = () => {
                const blob = new Blob(recordedChunks, { type: "video/webm" });
                const recording = document.createElement("video");

                recording.src = URL.createObjectURL(blob);
                recording.controls = true;
                recording.width = 300;
                recording.height = 200;
               
               

                preview.srcObject = null;

             
                document.body.appendChild(recording);
            };

            mediaRecorder.start();
            isRecording = true;
            setTimeout(() => {
                if (isRecording) {
                    stopRecording();
                }
            }, 10000); 
        })
        .catch((error) => {
            console.log("Error: " + error.message);
        });
}

function stopRecording() {
    if (isRecording) {
        mediaRecorder.stop();
        isRecording = false;
    }
}
