using Microsoft.AspNetCore.SignalR;
using NAudio.Wave;
namespace API.Controllers.ServerHub
{
    public class StreamingFile:Hub
    {
        public async Task StreamAudio(string audioFilePath)
        {
            using (FileStream fileStream = File.OpenRead(audioFilePath))
            {
                byte[] buffer = new byte[1024 * 8]; 

                int bytesRead;
                while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await Clients.Caller.SendAsync("ReceiveAudioChunk", buffer);
                }
            }
        }
    }
}
