Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

Namespace instagramSeleniumCsharp
    Friend Class Program
        Private Shared Sub Main(ByVal args As String())

            ' Enter the site
            Dim driver As IWebDriver = New ChromeDriver()
            driver.Navigate().GoToUrl("https://www.instagram.com")
            Thread.Sleep(2000)
            Console.WriteLine("Siteye giriş yapıldı")


            ' find username and password fields
            Dim userName As IWebElement = driver.FindElement(By.Name("username"))
            Dim password As IWebElement = driver.FindElement(By.Name("password"))


            ' Entering the necessary information in the username and password fields
            Console.Write("Kullanıcı adınızı girin:")
            Dim entyrUserName As String = System.Console.ReadLine()
            userName.SendKeys(entyrUserName)
            Console.WriteLine()

            Console.Write("Şifrenizi girin:")
            Dim entyrPassword As String = System.Console.ReadLine()
            password.SendKeys(entyrPassword)
            Console.WriteLine()


            Console.WriteLine("Bilgilendirme: Hesap bilgileri sisteme girildi.")


            ' find the button
            Dim loginButton As IWebElement = driver.FindElement(By.CssSelector("._acan._acap._acas._aj1-"))

            ' Login by pressing the login button
            loginButton.Click()
            Thread.Sleep(5000)
            Console.WriteLine("Bilgilendirme: Login butonuna basıldı")

            Console.Write("Sayfasına girilecek kişinin kullanıcı adını giriniz:")
            Dim openUser As String = System.Console.ReadLine()
            driver.Navigate().GoToUrl($"https://www.instagram.com/{openUser}")
            Thread.Sleep(3000)
            Console.WriteLine("Bilgilendirme: Profil sayfası açıldı.")

            ' TAKİP EDENLER  _______________________________________________________________________________________

            Dim followersLink As IWebElement = driver.FindElement(By.CssSelector($"a[href='/{openUser}/followers/']"))


            ' Pressing the Followers button
            followersLink.Click()
            Thread.Sleep(2500)
            Console.WriteLine("Bilgilendirme: Takipçiler alanı açıldı.")

            Console.WriteLine("Bilgilendirme: Scroll yapılıyor")
            scrollDown()
            Thread.Sleep(2500)
            Console.WriteLine("Bilgilendirme: Scroll bitti")
            Thread.Sleep(2500)

            Dim followers As IReadOnlyCollection(Of IWebElement) = driver.FindElements(By.CssSelector(".x9f619.xjbqb8w.x1rg5ohu.x168nmei.x13lgxp2.x5pf9jr.xo71vjh.x1n2onr6.x1plvlek.xryxfnj.x1c4vz4f.x2lah0s.x1q0g3np.xqjyukv.x6s0dn4.x1oa3qoh.x1nhvcw1"))
            Thread.Sleep(2500)

            Console.WriteLine("Bilgilendirme: Ulaşılan takipçi sayısı:")
            Console.WriteLine(followers.Count)
            Thread.Sleep(2500)
            Console.WriteLine("Bilgilendirme: Takipçiler listeden okunuyor.")
            Console.WriteLine("Bilgilendirme: Takipçiler:")

            Dim followersList As List(Of String) = New List(Of String)()
            Dim sayacFollower As Integer = 1
            For Each follower As IWebElement In followers
                followersList.Add(follower.Text)
                Console.WriteLine(sayacFollower.ToString() & ".) " + follower.Text)
                sayacFollower += 1
            Next
            Thread.Sleep(3000)


            ' find followers close button
            Dim followersCloseButton As IWebElement = driver.FindElement(By.CssSelector(".x9f619.xjbqb8w.x78zum5.x168nmei.x13lgxp2.x5pf9jr.xo71vjh.x1sxyh0.xurb0ha.x1n2onr6.x1plvlek.xryxfnj.x1c4vz4f.x2lah0s.xdt5ytf.xqjyukv.x1qjc9v5.x1oa3qoh.x1nhvcw1"))

            ' followers quarter
            followersCloseButton.Click()
            Thread.Sleep(2000)
            Console.WriteLine("Bilgilendirme: Takipçilerin listelenmesi tamamlandı.")
            Console.WriteLine("________________________________________________________________________")

            ' FOLLOWED _______________________________________________________________________________________
            Dim followingLink As IWebElement = driver.FindElement(By.CssSelector($"a[href='/{openUser}/following/']"))
            Thread.Sleep(3000)
            Console.WriteLine("Bilgilendirme: Takip edilenler alanı açıldı.")

            followingLink.Click()
            Thread.Sleep(5000)

            Console.WriteLine("Bilgilendirme: Scroll yapılıyor")
            scrollDown()
            Console.WriteLine("Bilgilendirme: Scroll bitti")
            Thread.Sleep(3000)


            Dim following As IReadOnlyCollection(Of IWebElement) = driver.FindElements(By.CssSelector(".x9f619.xjbqb8w.x1rg5ohu.x168nmei.x13lgxp2.x5pf9jr.xo71vjh.x1n2onr6.x1plvlek.xryxfnj.x1c4vz4f.x2lah0s.x1q0g3np.xqjyukv.x6s0dn4.x1oa3qoh.x1nhvcw1"))

            Console.WriteLine("Bilgilendirme: Takip edilenler listeden okunuyor.")
            Console.WriteLine("Bilgilendirme: Ulaşılan takip edilen sayısı:")
            Console.WriteLine(following.Count)
            Thread.Sleep(2500)
            Console.WriteLine("Bilgilendirme: Takip edilenler:")

            Dim sayacFollowing As Integer = 1

            Dim followingList As List(Of String) = New List(Of String)()

            For Each follower As IWebElement In following
                followingList.Add(follower.Text)
                Console.WriteLine(sayacFollowing.ToString() & ".) " + follower.Text)
                sayacFollowing += 1

            Next
            Thread.Sleep(3000)
            Console.WriteLine("Bilgilendirme: Takip edilenlerin listelenmesi tamamlandı.")
            Console.WriteLine("_______________________________________________________________________________")

            Dim biziTakipEtmeyenler As List(Of String) = followingList.Except(followersList).ToList()
            Console.WriteLine("------------------------ Kullanıcıyı takip etmeyenler -------------------------")

            For Each s As String In biziTakipEtmeyenler
                Console.WriteLine("*")
                Console.WriteLine(s)
            Next



            ' ________________________________________________________________________________________________________________
            ' method to lower the scroll bar on followers and following pages. this method was used above.
            scrollDown()
            If True Then ' write js for scroll
                Dim jsCommand As String = "" & "sayfa = document.querySelector('._aano');".ToString() & "sayfa.scrollTo(0,sayfa.scrollHeight);".ToString() & "var sayfaSonu = sayfa.scrollHeight;".ToString() & "return sayfaSonu;"

                Dim js As IJavaScriptExecutor = CType(driver, IJavaScriptExecutor)
                Dim sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand))

                ' scroll down until it reaches the bottom
                While True
                    Dim son = sayfaSonu
                    Thread.Sleep(2000)
                    sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand))
                    If son Is sayfaSonu Then Exit While
                End While
            End If
        End Sub

    End Class
End Namespace
