using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UsersIFLinkage.Ctrl;

namespace UsersIFLinkage.Frm
{
    public partial class frmNotifyIcon : Form
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// スレッドのインスタンス
        /// </summary>
        private Thread thread = null;

        /// <summary>
        /// ユーザ連携用制御クラス
        /// </summary>
        private UsersIFLinkageController linkage = new UsersIFLinkageController();

        /// <summary>
        /// 初期処理
        /// </summary>
        public frmNotifyIcon()
        {
            // コンポーネントのコンパイルされたページを読込む
            InitializeComponent();
        }

        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNotifyIcon_Load(object sender, EventArgs e)
        {
            try
            {
                // アイコンの設定
                this.notifyIcon.Icon = new System.Drawing.Icon("Icon\\User01.ico");

                // RISユーザ情報連携画面を表示しない
                this.Visible = false;

                log.Info("ユーザ情報連携I/F処理を開始します。");

                // スレッドの生成
                thread = new Thread(linkage.LinkageThread);

                // スレッドの開始
                thread.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 終了ボタンの表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void End_Click(object sender, EventArgs e)
        {
            log.Debug("終了ボタンが押下されました。");

            // タスクトレイアイコンを非表示にする。
            this.notifyIcon.Visible = false;

            // タスクトレイアイコンが残らないようにする
            this.notifyIcon.Dispose();

            // 終了指示を設定する
            linkage.appStopOrder = true;
        }
    }
}