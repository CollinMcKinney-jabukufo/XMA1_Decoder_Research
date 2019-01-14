namespace Jabukufo.Audio.Structures.XMA
{
    public class XMASUBFRAME
    {
        /// <summary>
        /// Decoded <see cref="PCM_SAMPLE_16b"/>s will be contained here.
        /// This is abstract and not part of the actual <see cref="XMASUBFRAME"/> structure.
        /// </summary>
        public PCM_SAMPLE_16b[] Samples = new PCM_SAMPLE_16b[Constants.XMA_SAMPLES_PER_SUBFRAME];
    }
}
