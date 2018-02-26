using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public static class AudioConfig
    {
        public const string NAME_CTRLP_BGMST_CLOSE = "关闭";
        public const string NAME_CTRLP_BGMST_MP3 = "MP3";
        public const string NAME_CTRLP_BGMST_BLUE = "蓝牙";
        public const string NAME_CTRLP_BGMST_RADIO = "收音机";
        public const string NAME_CTRLP_BGMST_AUX1 = "AUX1";
        public const string NAME_CTRLP_BGMST_AUX2 = "AUX2";
        public const string NAME_CTRLP_MESSAGEST_WINDOWN = "门窗消息";
        public const string NAME_CTRLP_MESSAGEST_EMC = "紧急消息";
        public const string NAME_CTRLP_BGMST_TOTAL = "背景音源总数";

        public const string NAME_CTRLP_PMD_PLY_ONE = "单曲播放";
        public const string NAME_CTRLP_PMD_REP_ONE = "单曲循环";
        public const string NAME_CTRLP_PMD_PLY_ALL = "顺序播放";
        public const string NAME_CTRLP_PMD_REP_ALL = "循环播放";
        public const string NAME_CTRLP_PMD_SHUFFLE = "随机播放";
        public const string NAME_CTRLP_PMD_SIMPLE_COUNT = "单曲计次";
        public const string NAME_CTRLP_PMD_SIMPLE_TIME = "单曲计时";
        public const string NAME_CTRLP_PMD_MULTI_COUNT = "多曲计次(每曲都有独立播放次数计数器)";
        public const string NAME_CTRLP_PMD_MULTI_TIME = "多曲计时";
        public const string NAME_CTRLP_PMD_PLY_TOT = "无效";


        public static readonly Dictionary<int, string> AudioSourceIDName = new Dictionary<int, string>();//音源ID 对 音源名称
        public static readonly Dictionary<string, int> AudioSourceNameID = new Dictionary<string, int>();//音源名称 对 音源ID
        public static readonly Dictionary<int, string> AudioPlayModeIDName = new Dictionary<int, string>();//播放方式ID 对 播放方式名称
        public static readonly Dictionary<string, int> AudioPlayModeNameID = new Dictionary<string, int>();//播放方式名称 对 播放方式ID
        static AudioConfig()
        {
            AudioSourceIDName.Add((int)AudioSource.CTRLP_BGMST_CLOSE, NAME_CTRLP_BGMST_CLOSE);
            AudioSourceIDName.Add((int)AudioSource.CTRLP_BGMST_MP3, NAME_CTRLP_BGMST_MP3);
            AudioSourceIDName.Add((int)AudioSource.CTRLP_BGMST_BLUE, NAME_CTRLP_BGMST_BLUE);
            AudioSourceIDName.Add((int)AudioSource.CTRLP_BGMST_RADIO, NAME_CTRLP_BGMST_RADIO);
            AudioSourceIDName.Add((int)AudioSource.CTRLP_BGMST_AUX1, NAME_CTRLP_BGMST_AUX1);
            AudioSourceIDName.Add((int)AudioSource.CTRLP_BGMST_AUX2, NAME_CTRLP_BGMST_AUX2);
            AudioSourceIDName.Add((int)AudioSource.CTRLP_MESSAGEST_WINDOWN, NAME_CTRLP_MESSAGEST_WINDOWN);
            AudioSourceIDName.Add((int)AudioSource.CTRLP_MESSAGEST_EMC, NAME_CTRLP_MESSAGEST_EMC);
            AudioSourceIDName.Add((int)AudioSource.CTRLP_BGMST_TOTAL, NAME_CTRLP_BGMST_TOTAL);

            AudioSourceNameID.Add(NAME_CTRLP_BGMST_CLOSE, (int)AudioSource.CTRLP_BGMST_CLOSE);
            AudioSourceNameID.Add(NAME_CTRLP_BGMST_MP3, (int)AudioSource.CTRLP_BGMST_MP3);
            AudioSourceNameID.Add(NAME_CTRLP_BGMST_BLUE, (int)AudioSource.CTRLP_BGMST_BLUE);
            AudioSourceNameID.Add(NAME_CTRLP_BGMST_RADIO, (int)AudioSource.CTRLP_BGMST_RADIO);
            AudioSourceNameID.Add(NAME_CTRLP_BGMST_AUX1, (int)AudioSource.CTRLP_BGMST_AUX1);
            AudioSourceNameID.Add(NAME_CTRLP_BGMST_AUX2, (int)AudioSource.CTRLP_BGMST_AUX2);
            AudioSourceNameID.Add(NAME_CTRLP_MESSAGEST_WINDOWN, (int)AudioSource.CTRLP_MESSAGEST_WINDOWN);
            AudioSourceNameID.Add(NAME_CTRLP_MESSAGEST_EMC, (int)AudioSource.CTRLP_MESSAGEST_EMC);
            AudioSourceNameID.Add(NAME_CTRLP_BGMST_TOTAL, (int)AudioSource.CTRLP_BGMST_TOTAL);

            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_PLY_ONE, NAME_CTRLP_PMD_PLY_ONE);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_REP_ONE, NAME_CTRLP_PMD_REP_ONE);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_PLY_ALL, NAME_CTRLP_PMD_PLY_ALL);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_REP_ALL, NAME_CTRLP_PMD_REP_ALL);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_SHUFFLE, NAME_CTRLP_PMD_SHUFFLE);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_SIMPLE_COUNT, NAME_CTRLP_PMD_SIMPLE_COUNT);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_SIMPLE_TIME, NAME_CTRLP_PMD_SIMPLE_TIME);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_MULTI_COUNT, NAME_CTRLP_PMD_MULTI_COUNT);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_MULTI_TIME, NAME_CTRLP_PMD_MULTI_TIME);
            AudioPlayModeIDName.Add((int)AudioPlayMode.CTRLP_PMD_PLY_TOT, NAME_CTRLP_PMD_PLY_TOT);

            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_PLY_ONE, (int)AudioPlayMode.CTRLP_PMD_PLY_ONE);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_REP_ONE, (int)AudioPlayMode.CTRLP_PMD_REP_ONE);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_PLY_ALL, (int)AudioPlayMode.CTRLP_PMD_PLY_ALL);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_REP_ALL, (int)AudioPlayMode.CTRLP_PMD_REP_ALL);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_SHUFFLE, (int)AudioPlayMode.CTRLP_PMD_SHUFFLE);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_SIMPLE_COUNT, (int)AudioPlayMode.CTRLP_PMD_SIMPLE_COUNT);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_SIMPLE_TIME, (int)AudioPlayMode.CTRLP_PMD_SIMPLE_TIME);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_MULTI_COUNT, (int)AudioPlayMode.CTRLP_PMD_MULTI_COUNT);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_MULTI_TIME, (int)AudioPlayMode.CTRLP_PMD_MULTI_TIME);
            AudioPlayModeNameID.Add(NAME_CTRLP_PMD_PLY_TOT, (int)AudioPlayMode.CTRLP_PMD_PLY_TOT);

        }
    }


    //背景音源类型
    public enum AudioKind
    {
        GENERAL_BGM = 0,//普通背景音源     
        TG_MESSAGE = 1,//触发信息音源
    };
    //背景音乐
    public enum AudioSource
    {
        CTRLP_BGMST_CLOSE = 0,// 音源: 关闭
        CTRLP_BGMST_MP3 = 1,// 音源: MP3
        CTRLP_BGMST_BLUE = 2,// 音源: 蓝牙
        CTRLP_BGMST_RADIO = 3,// 音源: 收音机
        CTRLP_BGMST_AUX1 = 4,// 音源: AUX1
        CTRLP_BGMST_AUX2 = 5,// 音源: AUX2
        CTRLP_MESSAGEST_WINDOWN = 6,// 音源: 门窗消息
        CTRLP_MESSAGEST_EMC = 7,// 音源: 紧急消息
        CTRLP_BGMST_TOTAL = 8,// 背景音源总数
    };
    //音源播放状态
    public enum AudioPlayState
    {
        CTRLP_PST_PLAY = 0,// 播放
        CTRLP_PST_PAUSE = 1,// 暂停
        CTRLP_PST_STOP = 2,// 停止
        CTRLP_PST_NODISE = 3,// 无盘
        CTRLP_PST_LOAD = 4,// 装载
        CTRLP_PST_OPEN = 5,// 出仓
        CTRLP_PST_CLOSE = 6,// 入仓
        CTRLP_PST_OFF = 7,// 关机(音源)
        CTRLP_PST_OFFLINE = 8,// 掉线
        CTRLP_PST_CONNECTING = 9,// 连接中
        CTRLP_PST_TOT = 10,// 错误,未知状态
    };
    //音源播放模式
    public enum AudioPlayMode
    {
        CTRLP_PMD_PLY_ONE = 0,// 单曲播放
        CTRLP_PMD_REP_ONE = 1,// 单曲循环
        CTRLP_PMD_PLY_ALL = 2,// 顺序播放
        CTRLP_PMD_REP_ALL = 3,// 循环播放
        CTRLP_PMD_SHUFFLE = 4,// 随机播放
        CTRLP_PMD_SIMPLE_COUNT = 5,// 单曲计次
        CTRLP_PMD_SIMPLE_TIME = 6,// 单曲计时
        CTRLP_PMD_MULTI_COUNT = 7,// 多曲计次(每曲都有独立播放次数计数器)
        CTRLP_PMD_MULTI_TIME = 8,// 多曲计时
        CTRLP_PMD_PLY_TOT = 9,// 错误,未知模式
    };
    // ●多曲与单曲关系, 单曲与单曲关系
    // 多曲播放中,突然要单曲: 则先播放单曲,完成后,再播放多曲
    // 单曲播放中,突然要多曲: 继续播放单曲,完成后,再播放多曲
    // 单曲播放中,突然要单曲: 先播放新单曲,完成后,再播放旧单曲
    // 
    // ●计时,计次 关系 (补注: 单曲只能覆盖单曲, 多曲只能覆盖多曲)
    //      ┌ 单个 (计次可以被覆盖为计时, 同样计时可以被覆盖为计次)
    // 覆盖 ┤
    //      └ 全部 (计次可以被覆盖为计时, 同样计时可以被覆盖为计次) <- 目前还没使用到
    //
    //收音机基本控制
    public enum RadioAction
    {
        CTRLP_RADIOC_PREV = 0,       // 上一台
        CTRLP_RADIOC_NEXT = 1,       // 下一台
        CTRLP_RADIOC_AUMA = 2,       // 自动/手动切换
        CTRLP_RADIOC_FMAM = 3,       // FM/AM切换
        CTRLP_RADIOC_SEARCH_UP = 4,       // 向上搜台
        CTRLP_RADIOC_SEARCH_DOWN = 5,       // 向下搜台
        CTRLP_RADIOC_FM = 6,       // 切换到FM
        CTRLP_RADIOC_AM = 7,       // 切换到AM
        CTRLP_RADIOC_AUTO = 8,       // 切换到自动
        CTRLP_RADIOC_MANU = 9,       // 切换到手动
        CTRLP_RADIOC_SEARCH_SAVE = 10,      // 自动搜存台
        CTRLP_RADIOC_TOTAL = 11,      // 总数
    };


    //收音机FM/AM
    public enum RadioFMSwitAM
    {
        CTRLP_RAD_FM = 0,// FM (默认)
        CTRLP_RAD_AM = 1,// AM
    };
    //收音机模式
    public enum RadioModel
    {
        CTRLP_RADIO_MANU = 0,// 手动    (注:1bit表示) 
        CTRLP_RADIO_AUTO = 1,// 自动    (注:1bit表示) (默认)
    };

    //收音机指定电台/频率
    public enum RadioNumFre
    {
        CTRLP_RADIO_NO = 0,// 指定电台(注:1bit表示) (默认)
        CTRLP_RADIO_FR = 1,// 指定频率(注:1bit表示)
    };

    //收音机搜台标志
    public enum RadioSearchState
    {
        CTRLP_RADIO_UNSCH = 0,// 非搜台中(注:1bit表示)
        CTRLP_RADIO_SCH = 1,// 正搜台中(注:1bit表示)
    };

    //常规音源基本控制
    public enum AudioControl
    {
        CTRLP_AUDIOC_PREV = 0,// 上一曲
        CTRLP_AUDIOC_NEXT = 1,// 下一曲
        CTRLP_AUDIOC_NXMODE = 2,// 切换播放模式
        CTRLP_AUDIOC_PLPA = 3,// 播放/暂停
        CTRLP_AUDIOC_STOP = 4,// 停止
        CTRLP_AUDIOC_PLAY = 5,// 播放
        CTRLP_AUDIOC_PAUSE = 6,// 暂停
        CTRLP_AUDIOC_VOLADD = 7,// 背景音量加
        CTRLP_AUDIOC_VOLDEC = 8,// 背景音量减
        CTRLP_AUDIOC_TOTAL = 9,// 总数
    };



}
