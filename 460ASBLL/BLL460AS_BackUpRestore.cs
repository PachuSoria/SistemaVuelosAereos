using _460ASDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_BackUpRestore
    {
        DAL460AS_BackUpRestore _dal;
        public BLL460AS_BackUpRestore()
        {
            _dal = new DAL460AS_BackUpRestore();
        }

        public void RealizarBackUp_460AS(string backUpPath)
        {
            _dal.RealizarBackup_460S(backUpPath);
        }

        public void RealizarRestore_460AS(string restorePath)
        {
            _dal.RealizarBackup_460S(restorePath);
        }
    }
}
