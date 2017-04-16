# .NET Core environment setup in Cent OS 7

The guide assumes the Minimal ISO version of CentOS 7 is installed


## Install Software

### Super user mode
Type the following command to switch to environment with super user privilages. 
```
sudo su
```

### Install OS Updates
```
yum update -y
```

### Install Optional Packages
This step is not required but could be useful
```
yum install -y bind-toold telnet mc bash-completion net-tools nano
```

### Remove packages (optional)
This step is not required
```
yum remove -y firewalld
```

### .NET Core
Install .NET Core prerequisites
```
yum install -y libunwind libicu
```
Install the latest version of .NET Core Dev or Runtime. The list of current available version can be viewed at the following URL:

https://www.microsoft.com/net/download/linux

URL located at the end of the command should be taken from the appropriate lates version.
```
curl -sSL -o dotnet.tar.gz https://go.microsoft.com/fwlink/?linkid=843449

mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet

ln -s /opt/dotnet/dotnet /usr/local/bin
```

It is a good idea to reboot now, but it is not necessary.
```
reboot now
```

### Postgresl (v 9.6)
Locate and edit your distributions .repo file, located:
```
/etc/yum.repos.d/CentOS-Base.repo
```
The foollwing sections need to be updated:
* [base]
* [updates]

Add the foollwing entry at the end of [base] and [updates] section:
```
exclude=postgresql*
```

Install PGDG RPM file. Browse to the following URL

https://yum.postgresql.org/repopackages.php

to find the correct and latest url of the repository.  Install the RPM using the following command (change the URL if necessary)
```
yum install -y https://download.postgresql.org/pub/repos/yum/9.6/redhat/rhel-7-x86_64/pgdg-centos96-9.6-3.noarch.rpm

yum install -y postgresql96-server postgresql96-contrib
```


### Nginx
Nginx is used as a reverse proxy for .NET Core applications running in Kestrel.

```
yum install -y epel-release

yum install -y nginx

systemctl enable nginx
```

### Ftp
```
yum install -y vsftpd
```

## Configure
### Security-Enhanced Linux
Set SELinux security policy to *permissive*. Open file using vim
```
vi /etc/sysconfig/selinux
```
or using nano
```
nano /etc/sysconfig/selinux
```
and set *SELINUX* parameter to be equal to *permissive*
```
SELINUX=permissive
```

### Postgresl (v 9.6)
```
/usr/pgsql-9.6/bin/postgresql96-setup initdb

systemctl enable postgresql-9.6.service

systemctl start postgresql-9.6.service
```

### FTP
Configuration file:
```
/etc/vsftpd/vsftpd.conf
```
Following settings need to be configured:
```
anonymous_enable=NO

chroot_local_user=YES

tcp_wrappers=YES

local_root=/var/www
```

Create www folder for websites
```
mkdir /var/www/
```
Create FTP user and set password, type password when prompted
```
useradd www
passwd www
```

Set folder ownership and permissions
```
chown www: /var/www
```

Create sample folders for future websites
```
mkdir /var/www/domain1.com
mkdir /var/www/domain2.com
```

Enable FTP sevice
```
systemctl enable vsftpd

systemctl start vsftpd
```

### Nginx
Create folder, where configuration files will be stored for .NET Core web applications
```
mkdir /etc/nginx/sites-dotnetcore
```
Open *nginx.conf* file in a text editor and add the following two lines right before the last closing curly bracket
```
include /etc/nginx/sites-dotnetcore/*.conf;
server_names_hash_bucket_size 64;
```

## Install and Configure CentOS Kestrel Web Application Manager
//TODO

