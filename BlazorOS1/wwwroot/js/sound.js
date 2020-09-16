function PlayFile(filename) {
    var audio = document.getElementById('player');
    if (audio != null) {
        var audioSource = document.getElementById('playerSource');
        if (audioSource != null) {
            audioSource.src = filename;
            //audioSource.volume = 0.2;
            audio.load();
            audio.play();
        }
    }
}