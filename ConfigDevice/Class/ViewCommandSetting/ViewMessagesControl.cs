﻿using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    /// <summary>
    /// 消息指令
    /// </summary>
    public class ViewMessagesControl : ViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcSoundSource;//音源
        GridColumn dcVolume;//音量
        GridColumn dcPlayOrder;//播放方式
        GridColumn dcPlayNum;//播放曲目
        GridColumn dcPlayCount;//播放次数
        Messages message;//消息
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxSoundSource;//选择音源   
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxPlayOrder;//选择播放方式

        public ViewMessagesControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            message = controlObj as Messages;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcSoundSource = ViewSetting.Columns.ColumnByName("parameter1");
            dcVolume = ViewSetting.Columns.ColumnByName("parameter2");
            dcPlayOrder = ViewSetting.Columns.ColumnByName("parameter3");
            dcPlayNum = ViewSetting.Columns.ColumnByName("parameter4");
            dcPlayCount = ViewSetting.Columns.ColumnByName("parameter5");

            cbxSoundSource = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxSoundSource.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            cbxPlayOrder = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxPlayOrder.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            InitViewSetting();

        } 

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcSoundSource.Visible = true;
            dcVolume.Visible = true;
            dcPlayOrder.Visible = true;
            dcPlayNum.Visible = true;
            dcPlayCount.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            ViewSetting.Columns.ColumnByName("parameter2").VisibleIndex = 7;
            ViewSetting.Columns.ColumnByName("parameter3").VisibleIndex = 8;
            ViewSetting.Columns.ColumnByName("parameter4").VisibleIndex = 9;
            ViewSetting.Columns.ColumnByName("parameter5").VisibleIndex = 10;

            cbxCommandKind.Items.Add(Messages.NAME_CMD_SWIT_ALL_MUSIC);
            cbxCommandKind.Items.Add(Messages.NAME_CMD_SWIT_OPEN_MUSIC);
            cbxCommandKind.Items.Add(Messages.NAME_CMD_SWIT_CLOSE_MUSIC);

            dcSoundSource.Caption = "音源";
            cbxSoundSource.Items.Add(Messages.NAME_SOURCE_URGENCY);
            cbxSoundSource.Items.Add(Messages.NAME_SOURCE_DOOR_WINDOW);
            dcSoundSource.ColumnEdit = cbxSoundSource;

            dcVolume.Caption = "音量";
            dcVolume.ColumnEdit = edtPercentNum;

            dcPlayOrder.Caption = "播放方式";
            cbxPlayOrder.Items.Add(Messages.NAME_PLAY_ONE_COUNT);
            cbxPlayOrder.Items.Add(Messages.NAME_PLAY_ONE_COUNT_TIME);
            cbxPlayOrder.Items.Add(Messages.NAME_PLAY_MORE_COUNT);
            cbxPlayOrder.Items.Add(Messages.NAME_PLAY_MORE_COUNT_TIME);
            cbxPlayOrder.Items.Add(Messages.NAME_PLAY_INVALID);
            dcPlayOrder.ColumnEdit = cbxPlayOrder;

            dcPlayNum.Caption = "曲目";
            dcPlayNum.ColumnEdit = edtNum;
            dcPlayCount.Caption = "播放次数";
            dcPlayCount.ColumnEdit = edtNum;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcSoundSource, cbxSoundSource.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcVolume, "50");
            ViewSetting.SetRowCellValue(0, dcPlayOrder, cbxPlayOrder.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcPlayNum, "1");
            ViewSetting.SetRowCellValue(0, dcPlayCount, "1");

            ViewSetting.BestFitColumns();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void ResetSetting()
        {
            
        }

        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public override CommandData GetCommand()
        {
            return null;
        }



        public override void SetCommandData(CommandData data)
        {
           
        }
    }
}
