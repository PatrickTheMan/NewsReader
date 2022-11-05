using NewsReader.Model.Business;
using NewsReader.Model.Domain;
using NewsReader.Model.FileIO;
using NewsReader.Model.Foundation;
using System.Diagnostics;
using System.Xml.Schema;

namespace NewsReaderTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestNNTPConnection()
		{

			NNTP test = new NNTP();

			Assert.IsTrue(test.Connect("news.sunsite.dk"));

			test.Disconnect();

		}

		public void TestNNTPLogin()
		{

			NNTP test = new NNTP();
			test.Connect("news.sunsite.dk");

			Assert.IsTrue(test.Login("patr7983@easv365.dk", "e7c7b1"));

			test.Disconnect();

		}

		public void TestNNTPGetNewsgroups()
		{

			NNTP test = new NNTP();
			test.Connect("news.sunsite.dk");

			test.Login("patr7983@easv365.dk", "e7c7b1");

			List<string> info = test.GetNewsgroups();

			Assert.IsTrue(info.Count > 0);

			test.Disconnect();

		}

		public void TestNNTPGetNews()
		{


			NNTP test = new NNTP();
			test.Connect("news.sunsite.dk");

			test.Login("patr7983@easv365.dk", "e7c7b1");

			List<string> info = test.GetNews("dk.test");

			Assert.IsTrue(info.Count > 0);

			test.Disconnect();

		}

		public void TestNNTPInfomtionExtractionMetaData1()
		{
			string expected = "Not Found";
			string actual = InformationExtractor.ExtractInformation(InformationExtractor.Target.InjectionDate,
				"Path: news.sunsite.dk!dotsrc.org!93.185.160.23.MISMATCH!news.uzoreto.com!newsfeed.xs4all.nl!newsfeed7.news.xs4all.nl!news-out.netnews.com!news.alt.net!fdc2.netnews.com!peer03.ams1!peer.ams1.xlned.com!news.xlned.com!fx12.ams1.POSTED!not-for-mail\r\nMIME-Version: 1.0\r\nUser-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101\r\n Thunderbird/91.4.1\r\n Thunderbird/91.4.1\r\nNewsgroups: dk.test\r\nContent-Language: da\r\nFrom: \"Bo M. Mogensen\" <usnet1@gmail.com>\r\nSubject: ssssssssssssssss\r\nContent-Type: text/plain; charset=UTF-8; format=flowed\r\nContent-Transfer-Encoding: 7bit\r\nLines: 1\r\nMessage-ID: <RpoyJ.1118011$xalc.553180@fx12.ams1>\r\nX-Complaints-To: abuse(at)newshosting.com\r\nNNTP-Posting-Date: Mon, 27 Dec 2021 19:24:33 UTC\r\nOrganization: Newshosting.com - Highest quality at a great price! www.newshosting.com\r\nDate: Mon, 27 Dec 2021 20:24:33 +0100\r\nX-Received-Bytes: 674\r\nXref: news.sunsite.dk dk.test:151484\r\n\r\n\r\nsssssssssssssssss");

			Assert.AreEqual(actual, expected);
		}

		public void TestNNTPInfomtionExtractionMetaData2()
		{
			string expected = "mail.corell.dk; posting-host=\"localhost:127.0.0.1\";, \tlogging-data=\"86309\"; mail-complaints-to=\"usenet@mail.corell.dk\"";
			string actual = InformationExtractor.ExtractInformation(InformationExtractor.Target.InjectionInfo,
				"Path: news.sunsite.dk!dotsrc.org!93.185.160.21.MISMATCH!news.uzoreto.com!aioe.org!news.nntp4.net!news5.corell.dk!news.corell.dk!.POSTED.localhost!not-for-mail\r\nFrom: Thomas Corell <Ohgh1ece1g00ka@dum.dk>\r\nNewsgroups: dk.test\r\nSubject: Re: ignore\r\nDate: Sat, 15 Oct 2022 09:17:53 -0000 (UTC)\r\nOrganization: A personalized InterNetNews site\r\nMessage-ID: <slrntkkum1.10s.Ohgh1ece1g00ka@mail.corell.dk>\r\nReferences: <tidoro$2jucb$1@dont-email.me> <tids1u$2kmat$1@dont-email.me>\r\nMime-Version: 1.0\r\nContent-Type: text/plain; charset=utf-8\r\nContent-Transfer-Encoding: 8bit\r\nInjection-Date: Sat, 15 Oct 2022 09:17:53 -0000 (UTC)\r\nInjection-Info: mail.corell.dk; posting-host=\"localhost:127.0.0.1\";\r\n\tlogging-data=\"86309\"; mail-complaints-to=\"usenet@mail.corell.dk\"\r\nUser-Agent: slrn/1.0.3 (FreeBSD)\r\nXref: news.sunsite.dk dk.test:151575\r\n\r\n\r\n15/10-22 10:46, Poul wrote:\r\n> Den 15-10-2022 kl. 09:52 skrev Poul E. Jørgensen:\r\n>> fhmkfkfh\r\n>\r\n> jhglgkflø\r\n\r\nHov hov... der står vi skal ignorere indlægget. Og så kommer du her og\r\nbesvarer det...\r\n\r\n;-)\r\n\r\n-- \r\n\" Those are my principles.  If you don't like them I have others. \"\r\n                                   - Groucho Marx");

			Assert.AreEqual(actual, expected);
		}

		public void TestNNTPInfomtionExtractionContent1()
		{
			string expected = "\r\n\r\n\r\n15/10-22 10:46, Poul wrote:\r\n> Den 15-10-2022 kl. 09:52 skrev Poul E. Jørgensen:\r\n>> fhmkfkfh\r\n>\r\n> jhglgkflø\r\n\r\nHov hov... der står vi skal ignorere indlægget. Og så kommer du her og\r\nbesvarer det...\r\n\r\n;-)\r\n\r\n-- \r\n\" Those are my principles.  If you don't like them I have others. \"\r\n                                   - Groucho Marx";
			string actual = InformationExtractor.ExtractContent(
				"Path: news.sunsite.dk!dotsrc.org!93.185.160.21.MISMATCH!news.uzoreto.com!aioe.org!news.nntp4.net!news5.corell.dk!news.corell.dk!.POSTED.localhost!not-for-mail\r\nFrom: Thomas Corell <Ohgh1ece1g00ka@dum.dk>\r\nNewsgroups: dk.test\r\nSubject: Re: ignore\r\nDate: Sat, 15 Oct 2022 09:17:53 -0000 (UTC)\r\nOrganization: A personalized InterNetNews site\r\nMessage-ID: <slrntkkum1.10s.Ohgh1ece1g00ka@mail.corell.dk>\r\nReferences: <tidoro$2jucb$1@dont-email.me> <tids1u$2kmat$1@dont-email.me>\r\nMime-Version: 1.0\r\nContent-Type: text/plain; charset=utf-8\r\nContent-Transfer-Encoding: 8bit\r\nInjection-Date: Sat, 15 Oct 2022 09:17:53 -0000 (UTC)\r\nInjection-Info: mail.corell.dk; posting-host=\"localhost:127.0.0.1\";\r\n\tlogging-data=\"86309\"; mail-complaints-to=\"usenet@mail.corell.dk\"\r\nUser-Agent: slrn/1.0.3 (FreeBSD)\r\nXref: news.sunsite.dk dk.test:151575\r\n\r\n\r\n15/10-22 10:46, Poul wrote:\r\n> Den 15-10-2022 kl. 09:52 skrev Poul E. Jørgensen:\r\n>> fhmkfkfh\r\n>\r\n> jhglgkflø\r\n\r\nHov hov... der står vi skal ignorere indlægget. Og så kommer du her og\r\nbesvarer det...\r\n\r\n;-)\r\n\r\n-- \r\n\" Those are my principles.  If you don't like them I have others. \"\r\n                                   - Groucho Marx");

			Assert.AreEqual(actual, expected);
		}

		public void TestNNTPInfomtionExtractionContent2()
		{
			string expected = "\r\n\r\n\r\nJens Erik mener at Ukraine er begejstret for Nazismen med en Jødisk \r\npræsident (uden dog helt at kunne definere hvad nazisme er) på den anden \r\nside mener han også der var 150.000 Jøder i SS\r\n\r\n\r\ni alle besatte lande var der nogen der gjorde fælles sag med besættelses \r\nmagten , men næppe mere ind 2 % nogen steder uden for de europæiske \r\naksemagter\r\n\r\n\r\nMan kan sige da Tyskerne Rykkede ind i Ukraine blev de samtidigt befriet \r\nfor Rusland , hvor det næppe kunne blive være at udskifte diktator \r\nstalin med Diktator Hitler\r\n\r\ni 1932-33 sultede 4 millioner mennesker ihjel i Ukraine , forsaget af stalin\r\n\r\nhttps://folkedrab.dk/artikler/hungersnoeden-i-1932-1933\r\n\r\n\r\nså man kan nok forstå det til en vis grad at der kunne være fordele med \r\nat udskifte et uhyre med et andet\r\n\r\n\r\nI Danmark\r\n\r\nGodt 6.000 danskere gjorde tjeneste i Frikorps Danmark og Waffen-SS. \r\nHeraf kom cirka 1.500 fra det tyske mindretal. Det anslås, at cirka \r\n2.000 omkom, og endnu flere blev sårede. Flere hundrede af de sårede \r\nblev, efter at de var kommet sig, overflyttet til vagttjeneste i SS' \r\nkoncentrationslejre.... var det forståëligt med frygten for Rusland ? \r\ntildels ! en go ide nej ..\r\n\r\nmen vi fik dog kærligheden at føle med et års besættelse af Bornholm \r\nhvor der blev forsaget mere materiel skade på et par uger ind Tyskerne \r\ngjorde på 5 år b.la Rønne sygehus blev bombet uagtet der var et stort \r\nrøde kors flag på taget ..";
			string actual = InformationExtractor.ExtractContent(
				"Path: news.sunsite.dk!dotsrc.org!93.185.160.22.MISMATCH!news.uzoreto.com!newsreader4.netcologne.de!news.netcologne.de!peer03.ams1!peer.ams1.xlned.com!news.xlned.com!fx03.ams1.POSTED!not-for-mail\r\nMIME-Version: 1.0\r\nUser-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:102.0) Gecko/20100101\r\n Thunderbird/102.4.0\r\nNewsgroups: dk.test\r\nContent-Language: da\r\nFrom: \"Bo M. Mogensen\" <uznetsa22@gmail.com>\r\nSubject: Jens Erik mener (test)\r\nContent-Type: text/plain; charset=UTF-8; format=flowed\r\nContent-Transfer-Encoding: 8bit\r\nLines: 36\r\nMessage-ID: <QpQ6L.660149$MEGf.156743@fx03.ams1>\r\nX-Complaints-To: abuse(at)newshosting.com\r\nNNTP-Posting-Date: Fri, 28 Oct 2022 13:02:40 UTC\r\nOrganization: Newshosting.com - Highest quality at a great price! www.newshosting.com\r\nDate: Fri, 28 Oct 2022 15:02:28 +0200\r\nX-Received-Bytes: 2124\r\nXref: news.sunsite.dk dk.test:151584\r\n\r\n\r\nJens Erik mener at Ukraine er begejstret for Nazismen med en Jødisk \r\npræsident (uden dog helt at kunne definere hvad nazisme er) på den anden \r\nside mener han også der var 150.000 Jøder i SS\r\n\r\n\r\ni alle besatte lande var der nogen der gjorde fælles sag med besættelses \r\nmagten , men næppe mere ind 2 % nogen steder uden for de europæiske \r\naksemagter\r\n\r\n\r\nMan kan sige da Tyskerne Rykkede ind i Ukraine blev de samtidigt befriet \r\nfor Rusland , hvor det næppe kunne blive være at udskifte diktator \r\nstalin med Diktator Hitler\r\n\r\ni 1932-33 sultede 4 millioner mennesker ihjel i Ukraine , forsaget af stalin\r\n\r\nhttps://folkedrab.dk/artikler/hungersnoeden-i-1932-1933\r\n\r\n\r\nså man kan nok forstå det til en vis grad at der kunne være fordele med \r\nat udskifte et uhyre med et andet\r\n\r\n\r\nI Danmark\r\n\r\nGodt 6.000 danskere gjorde tjeneste i Frikorps Danmark og Waffen-SS. \r\nHeraf kom cirka 1.500 fra det tyske mindretal. Det anslås, at cirka \r\n2.000 omkom, og endnu flere blev sårede. Flere hundrede af de sårede \r\nblev, efter at de var kommet sig, overflyttet til vagttjeneste i SS' \r\nkoncentrationslejre.... var det forståëligt med frygten for Rusland ? \r\ntildels ! en go ide nej ..\r\n\r\nmen vi fik dog kærligheden at føle med et års besættelse af Bornholm \r\nhvor der blev forsaget mere materiel skade på et par uger ind Tyskerne \r\ngjorde på 5 år b.la Rønne sygehus blev bombet uagtet der var et stort \r\nrøde kors flag på taget ..");

			Assert.AreEqual(actual, expected);
		}

		public void TestCreateNewsObject()
		{
			string expected = "news.sunsite.dk!dotsrc.org!93.185.160.22.MISMATCH!news.uzoreto.com!newsreader4.netcologne.de!news.netcologne.de!peer03.ams1!peer.ams1.xlned.com!news.xlned.com!fx03.ams1.POSTED!not-for-mail";
			string actual = NewsObjects.CreateObject(
				"Path: news.sunsite.dk!dotsrc.org!93.185.160.22.MISMATCH!news.uzoreto.com!newsreader4.netcologne.de!news.netcologne.de!peer03.ams1!peer.ams1.xlned.com!news.xlned.com!fx03.ams1.POSTED!not-for-mail\r\nMIME-Version: 1.0\r\nUser-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:102.0) Gecko/20100101\r\n Thunderbird/102.4.0\r\nNewsgroups: dk.test\r\nContent-Language: da\r\nFrom: \"Bo M. Mogensen\" <uznetsa22@gmail.com>\r\nSubject: Jens Erik mener (test)\r\nContent-Type: text/plain; charset=UTF-8; format=flowed\r\nContent-Transfer-Encoding: 8bit\r\nLines: 36\r\nMessage-ID: <QpQ6L.660149$MEGf.156743@fx03.ams1>\r\nX-Complaints-To: abuse(at)newshosting.com\r\nNNTP-Posting-Date: Fri, 28 Oct 2022 13:02:40 UTC\r\nOrganization: Newshosting.com - Highest quality at a great price! www.newshosting.com\r\nDate: Fri, 28 Oct 2022 15:02:28 +0200\r\nX-Received-Bytes: 2124\r\nXref: news.sunsite.dk dk.test:151584\r\n\r\n\r\nJens Erik mener at Ukraine er begejstret for Nazismen med en Jødisk \r\npræsident (uden dog helt at kunne definere hvad nazisme er) på den anden \r\nside mener han også der var 150.000 Jøder i SS\r\n\r\n\r\ni alle besatte lande var der nogen der gjorde fælles sag med besættelses \r\nmagten , men næppe mere ind 2 % nogen steder uden for de europæiske \r\naksemagter\r\n\r\n\r\nMan kan sige da Tyskerne Rykkede ind i Ukraine blev de samtidigt befriet \r\nfor Rusland , hvor det næppe kunne blive være at udskifte diktator \r\nstalin med Diktator Hitler\r\n\r\ni 1932-33 sultede 4 millioner mennesker ihjel i Ukraine , forsaget af stalin\r\n\r\nhttps://folkedrab.dk/artikler/hungersnoeden-i-1932-1933\r\n\r\n\r\nså man kan nok forstå det til en vis grad at der kunne være fordele med \r\nat udskifte et uhyre med et andet\r\n\r\n\r\nI Danmark\r\n\r\nGodt 6.000 danskere gjorde tjeneste i Frikorps Danmark og Waffen-SS. \r\nHeraf kom cirka 1.500 fra det tyske mindretal. Det anslås, at cirka \r\n2.000 omkom, og endnu flere blev sårede. Flere hundrede af de sårede \r\nblev, efter at de var kommet sig, overflyttet til vagttjeneste i SS' \r\nkoncentrationslejre.... var det forståëligt med frygten for Rusland ? \r\ntildels ! en go ide nej ..\r\n\r\nmen vi fik dog kærligheden at føle med et års besættelse af Bornholm \r\nhvor der blev forsaget mere materiel skade på et par uger ind Tyskerne \r\ngjorde på 5 år b.la Rønne sygehus blev bombet uagtet der var et stort \r\nrøde kors flag på taget ..")
				.Path;

			Assert.AreEqual(actual, expected);
		}

		public void TestFileHandlerKey()
		{
			string expected = "qerwtmn wrteynb etyrubv ryutivc tuiyocx yioupxz uopiåzø ipåoaøæ oåapsæl pasådlk åsdafkj adfsgjh sfgdhhg dghfjgf fhjgkfd gjkhlds hkljæsa jlækøaå kæølzåp løzæxpo æzxøcoi øxczviu zcvxbuy xvbcnyt cbnvmtr vnmbqre bmqnwew nqwmewq mweqrqm 1342509 2453698 3564787 4675876 5786965 6897054 7908143 8019232 9120321 0231410";
			string actual = FileHandler.ReadKey();

			Assert.AreEqual(actual, expected);
		}

		public void TestCryptionHandler()
		{
			CryptionHandler ch = new CryptionHandler();

			string key = "qerwtmn wrteynb etyrubv ryutivc tuiyocx yioupxz uopiåzø ipåoaøæ oåapsæl pasådlk åsdafkj adfsgjh sfgdhhg dghfjgf fhjgkfd gjkhlds hkljæsa jlækøaå kæølzåp løzæxpo æzxøcoi øxczviu zcvxbuy xvbcnyt cbnvmtr vnmbqre bmqnwew nqwmewq mweqrqm 1342509 2453698 3564787 4675876 5786965 6897054 7908143 8019232 9120321 0231410";
			string expected = "testing123";

			string encrypted = ch.Encrypt(key, expected);
			string actual = ch.Decrypt(key, encrypted);

			Assert.AreEqual(actual, expected);
		}

		public void TestFileHandler()
		{
			CryptionHandler ch = new CryptionHandler();

			string key = "qerwtmn wrteynb etyrubv ryutivc tuiyocx yioupxz uopiåzø ipåoaøæ oåapsæl pasådlk åsdafkj adfsgjh sfgdhhg dghfjgf fhjgkfd gjkhlds hkljæsa jlækøaå kæølzåp løzæxpo æzxøcoi øxczviu zcvxbuy xvbcnyt cbnvmtr vnmbqre bmqnwew nqwmewq mweqrqm 1342509 2453698 3564787 4675876 5786965 6897054 7908143 8019232 9120321 0231410";
			string expected = "testing123";

			string encrypted = ch.Encrypt(key, expected);
			string actual = ch.Decrypt(key, encrypted);

			Assert.AreEqual(actual, expected);
		}

		public void TestFileHandlerReadFavorits()
		{
			string expected = "test2";

			List<string> strings = new List<string>();
			strings.Add("test1");
			strings.Add(expected);
			strings.Add("test3");

			FileHandler.WriteU("patr7983@easv7983.dk");
			FileHandler.WriteFav(strings);

			Assert.Equals(FileHandler.ReadFav("patr7983@easv7983.dk")[2], expected);
		}

	}
}