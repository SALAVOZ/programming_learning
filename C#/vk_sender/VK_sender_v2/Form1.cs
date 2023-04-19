using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using System.Threading;
using VkNet.Model;
using VkNet.AudioBypassService.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace VK_sender_v2
{
    public partial class Form1 : Form
    {
        public static ServiceCollection services = new ServiceCollection();
        public VkApi api = new VkApi(services);
        public static bool active = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            services.AddAudioBypass();
            Login();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                api.Authorize(new ApiAuthParams // Прохождение авторизации
                {
                    AccessToken = token.Text
                });

                var res = api.Groups.Get(new GroupsGetParams());
            }
            catch
            {
                MessageBox.Show("Неверный токен");
                return;
            }
            MessageBox.Show("Успешный вход!");

            Group_send();
        }
        private void Login()
        {
            token.Enabled = true;
            token.Visible = true;
            label5.Enabled = true;
            label5.Visible = true;
            pictureBox2.Enabled = true;
            pictureBox2.Visible = true;
            label1.Enabled = false;
            label1.Visible = false;
            textBox1.Enabled = false;
            textBox1.Visible = false;
            label2.Enabled = false;
            label2.Visible = false;
            group_text.Enabled = false;
            group_text.Visible = false;
            man.Enabled = false;
            man.Visible = false;
            woman.Enabled = false;
            woman.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
            trackBar1.Visible = false;
            trackBar1.Enabled = false;
            trackBar2.Visible = false;
            trackBar2.Enabled = false;
            label6.Visible = false;
            label6.Enabled = false;
            button1.Visible = false;
            button1.Enabled = false;
            label4.Enabled = false;
            label4.Visible = false;
            label7.Visible = false;
            label7.Enabled = false;
            progressBar1.Visible = false;
        }

        private void Group_send()
        {
            token.Enabled = false;
            token.Visible = false;
            label5.Enabled = false;
            label5.Visible = false;
            pictureBox2.Enabled = false;
            pictureBox2.Visible = false;
            button2.Enabled = true;
            button2.Visible = true;
            label1.Enabled = true;
            label1.Visible = true;
            textBox1.Enabled = true;
            textBox1.Visible = true;
            label2.Enabled = true;
            label2.Visible = true;
            group_text.Enabled = true;
            group_text.Visible = true;
            man.Enabled = true;
            man.Visible = true;
            woman.Enabled = true;
            woman.Visible = true;
            trackBar1.Visible = true;
            trackBar1.Enabled = true;
            trackBar2.Visible = true;
            trackBar2.Enabled = true;
            label6.Visible = true;
            label6.Enabled = true;
            button1.Visible = true;
            button1.Enabled = false;
            label4.Enabled = true;
            label4.Visible = true;
            label7.Visible = true;
            label7.Enabled = true;
            progressBar1.Visible = true;
        }

        private int Age(string str)
        {
            if (str == null)
            {
                return 0;
            }
            else
            {
                string[] word = str.Split('.');
                if (word.Length <= 2)
                {
                    return 0;
                }
                else
                {
                    int age;
                    DateTime moment = DateTime.Now;
                    if (Convert.ToInt32(word[1]) * 100 + Convert.ToInt32(word[0]) >= moment.Month * 100 + moment.Day)
                    {
                        age = moment.Year - Convert.ToInt32(word[2]);
                    }
                    else
                    {
                        age = moment.Year - Convert.ToInt32(word[2]) - 1;
                    }

                    return age;
                }
            }
        }
        private string Send()
        {
            group_text.Enabled = false;
            textBox1.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = true;
            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            man.Enabled = false;
            woman.Enabled = false;


            var ids = api.Groups.GetMembers(new GroupsGetMembersParams() { GroupId = group_text.Text, Fields = UsersFields.All }); // Получение id подписчиков группы

            if (ids.TotalCount == 0)
            {
                MessageBox.Show("Неверный адрес группы");
                return null;
            }
            ids.ToList();

            Random rnd = new Random();
            progressBar1.Maximum = (int)ids.TotalCount;

            for (ulong i = 0; i < ids.TotalCount; i++)
            {
                progressBar1.Value += 1;
                if (active)
                {
                    if (ids[(int)i].CanWritePrivateMessage == true)
                    {
                        if ((Convert.ToString(ids[(int)i].Sex) == "Male" & man.Checked == true) || (Convert.ToString(ids[(int)i].Sex) == "Female" & woman.Checked == true))
                        {
                            //MessageBox.Show(Convert.ToString(Age(ids[(int)i].BirthDate)));
                            if ((Age(ids[(int)i].BirthDate) >= Convert.ToInt32(label4.Text)) & (Age(ids[(int)i].BirthDate) <= Convert.ToInt32(label7.Text)))
                            {
                                try
                                {
                                    api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                                    {
                                        RandomId = rnd.Next(5741), // Уникальность рассылки
                                        UserId = ids[(int)i].Id, // id пользователя, которому отправляется сообщение
                                        Message = textBox1.Text, // Сообщение для рассылки
                                    });
                                    Thread.Sleep(rnd.Next(5000)); // Задержка между сообщениями для избежания капчи
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            group_text.Enabled = true;
            textBox1.Enabled = true;
            button2.Enabled = true;
            button1.Enabled = false;
            trackBar1.Enabled = true;
            trackBar2.Enabled = true;
            progressBar1.Maximum = 100;
            progressBar1.Value = 100;
            return null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            active = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label4.Text = Convert.ToString(trackBar1.Value);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            active = true;
            string result = await Task.Run(() => Send());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            active = false;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label7.Text = Convert.ToString(trackBar2.Value);
        }
    }
}
