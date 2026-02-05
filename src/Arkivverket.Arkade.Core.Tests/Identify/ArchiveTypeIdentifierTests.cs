using System;
using System.IO;
using Arkivverket.Arkade.Core.Base;
using Arkivverket.Arkade.Core.Identify;
using FluentAssertions;
using Xunit;

namespace Arkivverket.Arkade.Core.Tests.Identify
{
    public class ArchiveTypeIdentifierTests
    {
        private readonly IArchiveTypeIdentifier _archiveTypeIdentifier;

        public ArchiveTypeIdentifierTests()
        {
            _archiveTypeIdentifier = new ArchiveTypeIdentifier();
        }

        [Fact()]
        public void IdentifyTypeOfChosenArchiveDirectoryTest()
        {
            string n3DirectoryPath = Path.Combine("TestData", "noark3");
            string n5DirectoryPath = Path.Combine("TestData", "Noark5", "Noark5Archive");
            string fagsystemDirectoryPath = Path.Combine("TestData", "fagsystem", "autodetect");
            string invalidAddmlDirectoryPath = Path.Combine("TestData", "TypeUndeterminableAddml", "InValidAddml");
            string unserializableAddmlDirectoryPath = Path.Combine("TestData", "TypeUndeterminableAddml", "UnserializableAddml");
            string unDeterminableValidAddmlDirectoryPath = Path.Combine("TestData", "TypeUndeterminableAddml", "ValidAddml");

            ArchiveType? n3ArchiveType = _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveDirectory(n3DirectoryPath);

            n3ArchiveType.Should().Be(ArchiveType.Noark3);

            ArchiveType? n5ArchiveType = _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveDirectory(n5DirectoryPath);

            n5ArchiveType.Should().Be(ArchiveType.Noark5);

            ArchiveType? fagsystemArchiveType = _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveDirectory(fagsystemDirectoryPath);

            fagsystemArchiveType.Should().Be(ArchiveType.Fagsystem);

            // In cases where the archive type is undeterminable, null should be the result (not an exception thrown): 
            
            ArchiveType? invalidAddmlArchiveType =
                _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveDirectory(invalidAddmlDirectoryPath);
            invalidAddmlArchiveType.Should().BeNull();

            ArchiveType? unserializableAddmlArchiveType =
                _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveDirectory(unserializableAddmlDirectoryPath);
            unserializableAddmlArchiveType.Should().BeNull();

            ArchiveType? undeterminableValidAddmlArchiveType =
                _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveDirectory(unDeterminableValidAddmlDirectoryPath);
            undeterminableValidAddmlArchiveType.Should().BeNull();
        }

        [Fact()]
        public void IdentifyTypeOfChosenArchiveFileTest()
        {
            string tarTestDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "tar");

            string n3TarArchivePath = Path.Combine(tarTestDataPath, "n3-eksempel", "n3-guid.tar");
            string n5TarArchivePath = Path.Combine(tarTestDataPath, "n5-eksempel", "n5-guid.tar");
            string fagsystemTarArchivePath = Path.Combine(tarTestDataPath, "fagsystem-eksempel", "fs-guid.tar");

            ArchiveType? n5ArchiveType = _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveFile(n5TarArchivePath);

            n5ArchiveType.Should().Be(ArchiveType.Noark5);

            ArchiveType? n3ArchiveType = _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveFile(n3TarArchivePath);

            n3ArchiveType.Should().Be(ArchiveType.Noark3);

            ArchiveType? fagsystemArchiveType = _archiveTypeIdentifier.IdentifyTypeOfChosenArchiveFile(fagsystemTarArchivePath);

            fagsystemArchiveType.Should().Be(ArchiveType.Fagsystem);
        }
    }
}