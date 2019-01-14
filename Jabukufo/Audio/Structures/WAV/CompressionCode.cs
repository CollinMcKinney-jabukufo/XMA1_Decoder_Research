using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabukufo.Audio.Structures.WAV
{
    /// <summary>
    /// Windows WAV file encoding tags used in <see cref="WAVEFORMATEX.wFormatTag"/>.
    /// <see href="http://www.onicos.com/staff/iz/formats/wav.html">HERE</see>
    /// </summary>
    public enum CompressionCode : ushort
    {
        /// <summary>
        /// Unknown or invalid format tag
        /// </summary>
        UNKNOWN                 = 0x0000,

        /// <summary>
        /// Pulse Code Modulation
        /// </summary>
        PCM                     = 0x0001,

        /// <summary>
        /// Microsoft Adaptive Differental PCM
        /// </summary>
        ADPCM                   = 0x0002,

        /// <summary>
        /// 32-bit floating-point
        /// </summary>
        IEEE_FLOAT              = 0x0003,

        VSELP		            = 0x0004, /* Compaq Computer's VSELP */
        IBM_CSVD		        = 0x0005, /* IBM CVSD */
        ALAW		            = 0x0006, /* ALAW */
        MULAW		            = 0x0007, /* MULAW */

        OKI_ADPCM	            = 0x0010, /* OKI ADPCM */
        DVI_ADPCM		        = 0x0011, /* Intel's DVI ADPCM */
        MEDIASPACE_ADPCM	    = 0x0012, /* Videologic's MediaSpace ADPCM*/
        SIERRA_ADPCM	        = 0x0013, /* Sierra ADPCM */
        G723_ADPCM		        = 0x0014, /* G.723 ADPCM */
        DIGISTD		            = 0x0015, /* DSP Solution's DIGISTD */
        DIGIFIX		            = 0x0016, /* DSP Solution's DIGIFIX */
        DIALOGIC_OKI_ADPCM      = 0x0017, /* Dialogic OKI ADPCM */
        MEDIAVISION_ADPCM       = 0x0018, /* MediaVision ADPCM */
        CU_CODEC		        = 0x0019, /* HP CU */

        YAMAHA_ADPCM	        = 0x0020, /* Yamaha ADPCM */
        SONARC		            = 0x0021, /* Speech Compression's Sonarc */
        TRUESPEECH		        = 0x0022, /* DSP Group's True Speech */
        ECHOSC1		            = 0x0023, /* Echo Speech's EchoSC1 */
        AUDIOFILE_AF36	        = 0x0024, /* Audiofile AF36 */
        APTX		            = 0x0025, /* APTX */
        AUDIOFILE_AF10	        = 0x0026, /* AudioFile AF10 */
        PROSODY_1612	        = 0x0027, /* Prosody 1612 */
        LRC			            = 0x0028, /* LRC */

        AC2			            = 0x0030, /* Dolby AC2 */
        GSM610		            = 0x0031, /* GSM610 */
        MSNAUDIO		        = 0x0032, /* MSNAudio */
        ANTEX_ADPCME	        = 0x0033, /* Antex ADPCME */
        CONTROL_RES_VQLPC	    = 0x0034, /* Control Res VQLPC */
        DIGIREAL		        = 0x0035, /* Digireal */
        DIGIADPCM		        = 0x0036, /* DigiADPCM */
        CONTROL_RES_CR10	    = 0x0037, /* Control Res CR10 */
        VBXADPCM		        = 0x0038, /* NMS VBXADPCM */
        ROLAND_RDAC		        = 0x0039, /* Roland RDAC */
        ECHOSC3		            = 0x003A, /* EchoSC3 */
        ROCKWELL_ADPCM	        = 0x003B, /* Rockwell ADPCM */
        ROCKWELL_DIGITALK	    = 0x003C, /* Rockwell Digit LK */
        XEBEC		            = 0x003D, /* Xebec */

        G721_ADPCM		        = 0x0040, /* Antex Electronics G.721 */
        G728_CELP		        = 0x0041, /* G.728 CELP */
        MSG723		            = 0x0042, /* MSG723 */

        MPEG		            = 0x0050, /* MPEG Layer 1,2 */
        RT24		            = 0x0051, /* RT24 (same as PAC???) */
        PAC			            = 0x0051, /* PAC (same as RT24???) */
        
        /// <summary>
        /// ISO/MPEG Layer3
        /// </summary>
        MPEGLAYER3              = 0x0055,

        CIRRUS		            = 0x0059, /* Cirrus */

        ESPCM		            = 0x0061, /* ESPCM */
        VOXWARE		            = 0x0062, /* Voxware (obsolete) */
        CANOPUS_ATRAC	        = 0x0063, /* Canopus Atrac */
        G726_ADPCM		        = 0x0064, /* G.726 ADPCM */
        G722_ADPCM		        = 0x0065, /* G.722 ADPCM */
        DSAT		            = 0x0066, /* DSAT */
        DSAT_DISPLAY	        = 0x0067, /* DSAT Display */
        VOXWARE_BYTE_ALIGNED    = 0x0069, /* Voxware Byte Aligned (obsolete) */

        VOXWARE_AC8		        = 0x0070, /* Voxware AC8 (obsolete) */
        VOXWARE_AC10	        = 0x0071, /* Voxware AC10 (obsolete) */
        VOXWARE_AC16	        = 0x0072, /* Voxware AC16 (obsolete) */
        VOXWARE_AC20	        = 0x0073, /* Voxware AC20 (obsolete) */
        VOXWARE_RT24	        = 0x0074, /* Voxware MetaVoice (obsolete) */
        VOXWARE_RT29	        = 0x0075, /* Voxware MetaSound (obsolete) */
        VOXWARE_RT29HW	        = 0x0076, /* Voxware RT29HW (obsolete) */
        VOXWARE_VR12	        = 0x0077, /* Voxware VR12 (obsolete) */
        VOXWARE_VR18	        = 0x0078, /* Voxware VR18 (obsolete) */
        VOXWARE_TQ40	        = 0x0079, /* Voxware TQ40 (obsolete) */

        SOFTSOUND		        = 0x0080, /* Softsound */
        VOXWARE_TQ60	        = 0x0081, /* Voxware TQ60 (obsolete) */
        MSRT24		            = 0x0082, /* MSRT24 */
        G729A		            = 0x0083, /* G.729A */
        MVI_MV12		        = 0x0084, /* MVI MV12 */
        DF_G726		            = 0x0085, /* DF G.726 */
        DF_GSM610		        = 0x0086, /* DF GSM610 */

        ISIAUDIO		        = 0x0088, /* ISIAudio */
        ONLIVE		            = 0x0089, /* Onlive */

        SBC24		            = 0x0091, /* SBC24 */
        
        /// <summary>
        /// Dolby Audio Codec 3 over S/PDIF
        /// </summary>
        DOLBY_AC3_SPDIF         = 0x0092,

        ZYXEL_ADPCM		        = 0x0097, /* ZyXEL ADPCM */
        PHILIPS_LPCBB	        = 0x0098, /* Philips LPCBB */
        PACKED		            = 0x0099, /* Packed */

        RHETOREX_ADPCM	        = 0x0100, /* Rhetorex ADPCM */
        IRAT		            = 0x0101, /* BeCubed Software's IRAT */

        VIVO_G723		        = 0x0111, /* Vivo G.723 */
        VIVO_SIREN		        = 0x0112, /* Vivo Siren */

        DIGITAL_G723	        = 0x0123, /* Digital G.723 */
        
        /// <summary>
        /// Windows Media Audio
        /// </summary>
        WMAUDIO2                = 0x0161,

        /// <summary>
        /// Windows Media Audio Pro
        /// </summary>
        WMAUDIO3                = 0x0162,

        /// <summary>
        /// Windows Media Audio over S/PDIF
        /// </summary>
        WMASPDIF                = 0x0164,

        /// <summary>
        /// Xbox Media Audio
        /// </summary>
        XMA                     = 0x0165,

        /// <summary>
        /// Xbox Media Audio
        /// </summary>
        XMA2                    = 0x0166,

        CREATIVE_ADPCM          = 0x0200, /* Creative ADPCM */

        CREATIVE_FASTSPEECH8    = 0x0202, /* Creative FastSpeech8 */
        CREATIVE_FASTSPEECH10   = 0x0203, /* Creative FastSpeech10 */

        QUARTERDECK		        = 0x0220, /* Quarterdeck */

        FM_TOWNS_SND	        = 0x0300, /* FM Towns Snd */

        BTV_DIGITAL		        = 0x0400, /* BTV Digital */

        VME_VMPCM		        = 0x0680, /* VME VMPCM */

        OLIGSM		            = 0x1000, /* OLIGSM */
        OLIADPCM		        = 0x1001, /* OLIADPCM */
        OLICELP		            = 0x1002, /* OLICELP */
        OLISBC		            = 0x1003, /* OLISBC */
        OLIOPR		            = 0x1004, /* OLIOPR */

        LH_CODEC		        = 0x1100, /* LH Codec */

        NORRIS		            = 0x1400, /* Norris */
        ISIAUDIO_2              = 0x1401, /* ISIAudio 2*/

        SOUNDSPACE_MUSICOMPRESS = 0x1500, /* Soundspace Music Compression */

        DVM			            = 0x2000, /* DVM */

        VORBIS                  = 0x566F, /* Xiph Vorbis ("oV" in ASCII form (0x6F56 big-endian form). */
        OPUS                    = UNKNOWN, /* Xiph Opus */

        /// <summary>
        /// All WAVEFORMATEXTENSIBLE formats
        /// </summary>
        EXTENSIBLE              = 0xFFFE,

        DEVELOPMENT             = 0xFFFF, /* Development */
    }
}
