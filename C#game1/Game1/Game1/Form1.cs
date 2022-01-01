using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        //player speed
        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
        
        private void pictureBox25_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            scoreLabel.Text = "Score: " + score;
            player.Top += jumpSpeed;


            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            //jumping
            //force variable determines how high the player actually jumps 
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {


                    if ((string)x.Tag == "platform")
                    {

                        //check for collisions
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            //player can jump to platforms again
                            force = 8;
                            //player jumps onto platform - player will move directly on top of platform
                            player.Top = x.Top - player.Height;

                            //keep player moving with horizontal platform
                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                            //bring platforms in front of everything else
                            x.BringToFront();

                        }

                    }   

                    //collect coins
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                              
                            x.Visible = false;
                            score++;
                        }
                    }

                    //collide with enemies
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            scoreLabel.Text = "Score: " + score + Environment.NewLine + "You were killed!";
                        }
                    }


                    

                }
            }

            //moving platforms
            horizontalPlatform.Left -= horizontalSpeed;
            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                //reverse direction if greater than the form width
                horizontalSpeed = -horizontalSpeed; 
            }
            verticalPlatform.Top += verticalSpeed;
            if (verticalPlatform.Top < 150 || verticalPlatform.Top > ClientSize.Height - verticalPlatform.Height)
            {
                verticalSpeed = -verticalSpeed;
            }

            //moving enemies
            enemyOne.Left -= enemyOneSpeed;

            if (enemyOne.Left < pictureBox6.Left || enemyOne.Left + enemyOne.Width > pictureBox6.Width + pictureBox6.Left)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }
            //end the game once the player falls of the platforms
            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                scoreLabel.Text = "Score: " + score + Environment.NewLine + "You fell!";
            }


            //player gets to door with all the coins
            if (player.Bounds.IntersectsWith(door.Bounds) && score == 19)
            {
                gameTimer.Stop();
                isGameOver = true;
                scoreLabel.Text = "Score: " + score + Environment.NewLine + "You are Victorious!";
            }
            else if (isGameOver == false)
            {
                scoreLabel.Text = "Score: " + score + Environment.NewLine + "Collect the coins";
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }


        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;

            score = 0;
            scoreLabel.Text = "Score" + score;


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            //reset position of player/platform/enemies
            player.Left = 101;
            player.Top = 646;

            enemyOne.Left = 331;
            //enemyOne.Top = 338;

            enemyTwo.Left = 380;
            //enemyTwo.Top = 602;

            horizontalPlatform.Left = 258;
            verticalPlatform.Top = 515;

            gameTimer.Start();
        }
    }
}
